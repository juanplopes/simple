using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicLibrary.Threading;
using SimpleLibrary.DataAccess;
using NHibernate;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace SimpleLibrary.Threading
{
    public class NHLockingProvider : SqlLockingProvider<NHLockToken, ISession>
    {
        protected override string TableName
        {
            get { return "instance_state"; }
        }

        protected override string TypeColumn
        {
            get { return "type"; }
        }

        protected override string IdColumn
        {
            get { return "id"; }
        }

        protected override string SemaphoreColumn
        {
            get { return "semaphore"; }
        }

        protected override string DataColumn
        {
            get { return "data"; }
        }

        protected override int DefaultTimeout
        {
            get { return 10; }
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
