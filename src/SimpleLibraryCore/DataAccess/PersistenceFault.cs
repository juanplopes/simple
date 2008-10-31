using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace SimpleLibrary.DataAccess
{
    [DataContract]
    public class PersistenceFault : GenericFault
    {
        public enum ReasonType
        {
            OptimisticLockFailed,
            ADOException,
            ObjectNotFound
        }

        public ReasonType GetReason()
        {
            return this.GetReason<ReasonType>();
        }

        public PersistenceFault() : base() { }
        public PersistenceFault(ReasonType reason) : base((int)reason) { }
        public PersistenceFault(ReasonType reason, object information) : base((int)reason, information) { }
    }
}
