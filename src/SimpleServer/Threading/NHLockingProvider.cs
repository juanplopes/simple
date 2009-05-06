using System;
using System.Collections.Generic;

using System.Text;
using Simple.Threading;
using Simple.DataAccess;
using NHibernate;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Simple.Config;

namespace Simple.Threading
{
    public class NHLockingProvider : SqlLockingProvider<NHLockToken, ISession>
    {
        protected SimpleConfig Config = SimpleConfig.Get();

        protected override string TableName
        {
            get { return Config.Threading.LockingProvider.TableName; }
        }

        protected override string TypeColumn
        {
            get { return Config.Threading.LockingProvider.TypeColumn; }
        }

        protected override string IdColumn
        {
            get { return Config.Threading.LockingProvider.IdColumn; }
        }

        protected override string SemaphoreColumn
        {
            get { return Config.Threading.LockingProvider.SemaphoreColumn; }
        }

        protected override string DataColumn
        {
            get { return Config.Threading.LockingProvider.DataColumn; }
        }

        protected override int DefaultTimeout
        {
            get { return Config.Threading.LockingProvider.DefaultTimeout; }
        }

        protected override object ExecuteQuery(ISession transaction, int timeout, string sqlQuery, params object[] parameter)
        {
            ISQLQuery query = transaction.CreateSQLQuery(sqlQuery);
            query.SetTimeout(timeout);
            for (int i = 0; i < parameter.Length; i++)
            {
                query.SetParameter(i, parameter[i]);
            }
            return query.UniqueResult();
        }

        protected override int ExecuteNonQuery(ISession transaction, int timeout, string sqlQuery, params object[] parameter)
        {
            ISQLQuery query = transaction.CreateSQLQuery(sqlQuery);
            query.SetTimeout(timeout);
            for (int i = 0; i < parameter.Length; i++)
            {
                query.SetParameter(i, parameter[i]);
            }
            return query.ExecuteUpdate();
        }

        protected override ISession CreateTransaction()
        {
            ISession session = SessionManager.GetSession(true);
            session.BeginTransaction();
            return session;
        }

        protected override NHLockToken CreateLockToken(string type, int id)
        {
            return new NHLockToken(type, id);
        }
    }
}
