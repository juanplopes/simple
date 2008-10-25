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
    public class NHLockingProvider : BaseLockingProvider<NHLockToken>, IDataStoreLockingProvider<NHLockToken>
    {
        public T GetData<T>(NHLockToken token) where T : BasicLibrary.Common.IInitializable, new()
        {
            if (token.Data == null) return new T();
            
            return (T)token.Data;
        }

        public void SetData(NHLockToken token, object obj)
        {
            byte[] data = null;
            if (obj != null)
            {
                BinaryFormatter serializer = new BinaryFormatter();
                MemoryStream stream = new MemoryStream();
                serializer.Serialize(stream, obj);
                data = stream.GetBuffer();
            }

            NHLockRow row = new NHLockRow()
            {
                Id = new NHLockId(token.Type, token.Id),
                Data = data
            };

            token.Session.SaveOrUpdate(row);
            token.Session.Flush();
            token.Session.Transaction.Commit();
            token.Session.Transaction.Begin();
        }

        public void RemoveData(NHLockToken token)
        {
            NHLockRow row = token.Session.Load<NHLockRow>(new NHLockId(token.Type, token.Id));
            token.Session.Delete(row);
        }

        protected override void EnsureLock(NHLockToken token, int secondsToWait)
        {
            try
            {
                NHLockRow row = token.Session.Load<NHLockRow>(new NHLockId(token.Type, token.Id), LockMode.Upgrade);
                if (row.Data != null)
                {
                    byte[] byteArray = (byte[])row.Data;
                    BinaryFormatter deserializer = new BinaryFormatter();
                    MemoryStream stream = new MemoryStream(byteArray);
                    token.Data = deserializer.Deserialize(stream);
                }
                else
                {
                    token.Data = null;
                }

            }
            catch (ObjectNotFoundException)
            {
                token.Data = null;
            }
        }

        protected override NHLockToken CreateLockToken(string type, int id)
        {
            ISession session = SessionManager.GetSession();
            session.BeginTransaction();
            return new NHLockToken(session, type, id);
        }
    }
}
