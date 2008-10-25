using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicLibrary.Threading;
using NHibernate;

namespace SimpleLibrary.Threading
{
    public class NHLockToken : BaseLockToken, IDataStoreLockToken
    {
        public ISession Session { get; set; }

        public override bool ValidState
        {
            get { return Session != null; }
        }

        public object Data { get; set; }

        public NHLockToken(ISession session, string type, int id)
            : base(type, id)
        {
            Session = session;
        }
    }
}
