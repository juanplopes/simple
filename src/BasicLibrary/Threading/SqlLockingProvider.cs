using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using BasicLibrary.Common;

namespace BasicLibrary.Threading
{
    public abstract class SqlLockingProvider<TokenType, TransactionType> : BaseLockingProvider<TokenType>,
        IDataStoreLockingProvider<TokenType>
    where TokenType : SqlLockToken<TransactionType>
    {
        protected abstract string TableName { get; }
        protected abstract string TypeColumn { get; }
        protected abstract string IdColumn { get; }
        protected abstract string SemaphoreColumn { get; }
        protected abstract string DataColumn { get; }
        protected abstract int DefaultTimeout { get; }

        protected abstract object ExecuteQuery(TransactionType transaction, int timeout, string sqlQuery, params object[] parameter);
        protected abstract int ExecuteNonQuery(TransactionType transaction, int timeout, string sqlQuery, params object[] parameter);
        protected abstract TransactionType CreateTransaction();

        protected int GetTimeout(int value)
        {
            if (value < 0) return DefaultTimeout;
            else return value;
        }

        protected override void EnsureLock(TokenType token, int timeout)
        {
            timeout = GetTimeout(timeout);

            if (token == null) throw new InvalidOperationException("Invalid locking token");

            if (TryUpdate(token, token.Type, token.Id, timeout) == 0)
                Insert(token, token.Type, token.Id, timeout);
        }

        protected int TryUpdate(TokenType token, string type, int id, int timeout)
        {
            timeout = GetTimeout(timeout);

            string sqlQuery =
                TranslateSqlString(@"UPDATE :table:  SET :semaphore:=1-:semaphore: 
                                                           WHERE :id:=? AND :type:=?");

            return ExecuteNonQuery(token.Transaction, GetTimeout(timeout), sqlQuery, id, type);

        }

        protected int Insert(TokenType token, string type, int id, int timeout)
        {
            timeout = GetTimeout(timeout);

            string sqlQuery = TranslateSqlString(@"INSERT INTO :table:(:id:, :type:) 
                                                       VALUES (?, ?)");

            return ExecuteNonQuery(token.Transaction, GetTimeout(timeout), sqlQuery, id, type);
        }

        public T GetData<T>(TokenType token) where T : IInitializable, new()
        {
            string sqlQuery = TranslateSqlString(
                                        @"SELECT :data: AS data FROM :table:
                                        WHERE :id:=? AND :type:=?");

            object lobjDBData = ExecuteQuery(token.Transaction, DefaultTimeout, sqlQuery, token.Id, token.Type);
            T ret;

            if (lobjDBData != DBNull.Value)
            {
                byte[] lobjData = (byte[])lobjDBData;
                BinaryFormatter lobjDeserializer = new BinaryFormatter();
                MemoryStream lobjStream = new MemoryStream(lobjData);
                ret = (T)lobjDeserializer.Deserialize(lobjStream);
            }
            else
            {
                ret = new T();
                ret.Init();
            }

            token.Data = ret;

            return ret;
        }

        public override void Release(TokenType token)
        {
            lock (token)
            {
                token.Commit();
                base.Release(token);

                if (token.ConnectedClients > 0)
                {
                    token.BeginTransaction();
                    EnsureLock(token, TimeoutValues.DefaultWait);
                }
                else
                {
                    token.Transaction = default(TransactionType);
                }
            }

        }

        public void SetData(TokenType token, object obj)
        {
            if (!object.ReferenceEquals(token.Data, obj)) throw new InvalidOperationException("Cannot persist this object. It is invalid");

            string sqlQuery = TranslateSqlString(
                                        @"UPDATE :table: 
                                          SET :data:=?
                                          WHERE :id:=? AND :type:=?");
            object data = null;
            if (obj != null)
            {
                BinaryFormatter lobjSerializer = new BinaryFormatter();
                MemoryStream lobjStream = new MemoryStream();
                lobjSerializer.Serialize(lobjStream, obj);
                data = lobjStream.GetBuffer();
            }

            ExecuteNonQuery(token.Transaction, TimeoutValues.DefaultWait, sqlQuery, data, token.Id, token.Type);

            Release(token);
        }

        protected string TranslateSqlString(string sql)
        {
            StringBuilder builder = new StringBuilder(sql);

            builder.Replace(":id:", IdColumn);
            builder.Replace(":semaphore:", SemaphoreColumn);
            builder.Replace(":type:", TypeColumn);
            builder.Replace(":table:", TableName);
            builder.Replace(":data:", DataColumn);

            return builder.ToString();
        }

        public void RemoveData(TokenType token)
        {
            SetData(token, null);
        }

    }
}
