using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using Simple.ServiceModel;

namespace Simple.DataAccess
{
    [DataContract, SimpleFaultContract]
    public class PersistenceFault : GenericFault<PersistenceFault.ReasonType>
    {
        public enum ReasonType
        {
            OptimisticLockFailed,
            ADOException,
            ObjectNotFound
        }

        public PersistenceFault() : base() { }
        public PersistenceFault(ReasonType reason) : base(reason) { }
        public PersistenceFault(ReasonType reason, object information) : base(reason, information) { }
    }
}
