using System;
using System.Collections.Generic;

using System.Text;
using NHibernate;
using SimpleLibrary.DataAccess;
using System.ServiceModel;
using BasicLibrary.Logging;
using SimpleLibrary.Rules;

namespace SimpleLibrary.DataAccess
{
    public class DefaultExceptionHandler : IExceptionHandler
    {
        public static DefaultExceptionHandler Instance { get { return Nested.This; } }

        protected static class Nested
        {
            public static DefaultExceptionHandler This = new DefaultExceptionHandler();
        }

        public bool Handle(Exception e)
        {
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
            PersistenceFault fault = new PersistenceFault(reason,info);

            FaultException<PersistenceFault> ex = new FaultException<PersistenceFault>(fault, 
                new FaultReason( fault.Reason.ToString() + ": " + fault.Information.ToString()));
            return ex;
        }

        #region IExceptionHandler Members


        public IEnumerable<Type> IdentifyThrowingTypes()
        {
            yield return typeof(PersistenceFault);
        }

        #endregion
    }
}
