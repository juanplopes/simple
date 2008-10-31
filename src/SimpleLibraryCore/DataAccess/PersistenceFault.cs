using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using SimpleLibrary.ServiceModel;

namespace SimpleLibrary.DataAccess
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
