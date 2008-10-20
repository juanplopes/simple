using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace SimpleLibrary.DataAccess
{
    [DataContract]
    public class PersistenceFault
    {
        public enum ReasonType
        {
            OptimisticLockFailed,
            ADOException,
            ObjectNotFound
        }

        [DataMember]
        public ReasonType Reason { get; set; }
        [DataMember]
        public object Information { get; set; }
    }
}
