using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using SimpleLibrary.DataAccess;
using System.ServiceModel;
using BasicLibrary.Logging;

namespace SimpleLibrary.ServiceModel
{
    public class DefaultExceptionHandler
    {
        public static bool Handle(Exception e)
        {
            MainLogger.Default.Warn("Handling exception: " + e.ToString());
            if (e is StaleObjectStateException)
            {
                throw CreatePersistenceFault(
                    PersistenceFault.ReasonType.OptimisticLockFailed,
                    (e as StaleObjectStateException).Message);
            }
            else if (e is ADOException)
            {
                throw CreatePersistenceFault(
                    PersistenceFault.ReasonType.ADOException,
                    (e as ADOException).Message);
            }
            else if (e is ObjectNotFoundException)
            {
                throw CreatePersistenceFault(
                    PersistenceFault.ReasonType.ObjectNotFound,
                    (e as ObjectNotFoundException).Message);

            }
            return false;
        }

        protected static FaultException<PersistenceFault> CreatePersistenceFault(PersistenceFault.ReasonType reason, object info)
        {
            PersistenceFault fault = new PersistenceFault();
            fault.Reason = reason;
            fault.Information = info;

            FaultException<PersistenceFault> ex = new FaultException<PersistenceFault>(fault, 
                new FaultReason( fault.Reason.ToString() + ": " + fault.Information.ToString()));
            return ex;
        }
    }
}
