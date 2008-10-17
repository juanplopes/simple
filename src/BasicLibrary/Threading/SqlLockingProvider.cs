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
    public abstract class SqlLockingProvider :
        BaseLockingProvider<SqlLockToken>,
        IDataStoreLockingProvider<SqlLockToken>
    {
        public const int DEFAULT_WAIT = -1;

        protected abstract string ConnectionString { get; }
        protected abstract string TableName { get; }
        protected abstract string TypeColumn { get; }
        protected abstract string IdColumn { get; }
        protected abstract string SemaphoreColumn { get; }
        protected abstract string DataColumn { get; }
        protected abstract int SecondsToWait { get; }

        protected override void EnsureLock(SqlLockToken token, int secondsToWait)
        {
            if (token == null) throw new InvalidOperationException("Invalid locking token");

            if (secondsToWait < 0) secondsToWait = SecondsToWait;

            try
            {
                if (TryUpdate(token, token.Type, token.Id, secondsToWait) == 0)
                    Insert(token, token.Type, token.Id, secondsToWait);
            }
            catch (SqlException e)
            {
                if (e.Number == 1222)
                    throw new UnableToAcquireLockException(token.Type, token.Id);
                else
                    throw;
            }
        }

        protected SqlTransaction CreateTransaction()
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        protected override SqlLockToken CreateLockToken(string type, int id)
        {
            SqlLockToken token = new SqlLockToken(CreateTransaction(), type, id);
            return token;
        }

        protected int TryUpdate(SqlLockToken token, string type, int id, int secondsToWait)
        {
            if (secondsToWait < 0) secondsToWait = SecondsToWait;

            SqlConnection connection = token.Transaction.Connection;

            SqlCommand command = connection.CreateCommand();
            command.CommandTimeout = secondsToWait;
            command.CommandText = string.Format(
                TranslateSqlString(@"UPDATE :table: WITH(ROWLOCK{0})    
                                                           SET :semaphore:=1-:semaphore: 
                                                           WHERE :id:=@id AND :type:=@type"), (secondsToWait == 0 ? ",NOWAIT" : ""));
            command.Parameters.Add(new SqlParameter("id", id));
            command.Parameters.Add(new SqlParameter("type", type));
            command.Transaction = token.Transaction;
            return command.ExecuteNonQuery();
        }

        protected int Insert(SqlLockToken token, string type, int id, int secondsToWait)
        {
            if (secondsToWait < 0) secondsToWait = SecondsToWait;

            SqlConnection connection = token.Transaction.Connection;

            SqlCommand command = connection.CreateCommand();
            command.CommandTimeout = secondsToWait;
            command.CommandText = string.Format(TranslateSqlString(@"INSERT INTO :table: WITH(ROWLOCK{0}) (:id:, :type:) 
                                                       VALUES (@id, @type)"), (secondsToWait == 0 ? ",NOWAIT" : ""));
            command.Parameters.Add(new SqlParameter("id", id));
            command.Parameters.Add(new SqlParameter("type", type));
            command.Transaction = token.Transaction;
            return command.ExecuteNonQuery();
        }

        public T GetData<T>(SqlLockToken token) where T : IInitializable, new()
        {
            if (token.Data != null && token.Data is T) return (T)token.Data;

            SqlConnection connection = token.Transaction.Connection;
            SqlCommand command = connection.CreateCommand();
            command.CommandText = TranslateSqlString(
                                        @"SELECT :data: AS data FROM :table:
                                        WHERE :id:=@id AND :type:=@type");
            command.Parameters.Add(new SqlParameter("id", token.Id));
            command.Parameters.Add(new SqlParameter("type", token.Type));
            command.Transaction = token.Transaction;

            object lobjDBData = command.ExecuteScalar();
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

        public override void Release(SqlLockToken token)
        {
            lock (token)
            {
                SqlConnection connection = token.Transaction.Connection;
                token.Transaction.Commit();
                connection.Close();

                token.Transaction.Dispose();
                base.Release(token);
                if (token.ConnectedClients > 0)
                {
                    token.Transaction = CreateTransaction();
                    EnsureLock(token, DEFAULT_WAIT);
                }
                else
                {
                    token.Transaction = null;
                }
            }

        }

        public void SetData(SqlLockToken token, object obj)
        {
            if (!object.ReferenceEquals(token.Data, obj)) throw new InvalidOperationException("Cannot persist this object. It is invalid");

            SqlConnection lobjConnection = token.Transaction.Connection;

            SqlCommand lobjCommand = lobjConnection.CreateCommand();
            lobjCommand.CommandText = TranslateSqlString(
                                        @"UPDATE :table: 
                                          SET :data:=@data
                                          WHERE :id:=@id AND :type:=@type");
            object data = null;
            if (obj != null)
            {
                BinaryFormatter lobjSerializer = new BinaryFormatter();
                MemoryStream lobjStream = new MemoryStream();
                lobjSerializer.Serialize(lobjStream, obj);
                data = lobjStream.GetBuffer();
            }

            lobjCommand.Parameters.Add(new SqlParameter("id", token.Id));
            lobjCommand.Parameters.Add(new SqlParameter("type", token.Type));
            lobjCommand.Parameters.Add(new SqlParameter("data", data!=null?data:DBNull.Value));
            lobjCommand.Transaction = token.Transaction;

            lobjCommand.ExecuteNonQuery();

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

        public void RemoveData(SqlLockToken token)
        {
            SetData(token, null);
        }

    }
}
