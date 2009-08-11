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
                new PersistenceFault(
                    PersistenceFault.ReasonType.OptimisticLockFailed,
                    e.Message).Throw();
                return true;
            }
            else if (e is ADOException)
            {
                new PersistenceFault(
                    PersistenceFault.ReasonType.ADOException,
                    e.Message).Throw();
                return true;
            }
            else if (e is ObjectNotFoundException)
            {
                new PersistenceFault(
                    PersistenceFault.ReasonType.ObjectNotFound,
                    e.Message).Throw();
                return true;
            }
            else if (e is TransientObjectException)
            {
                new PersistenceFault(PersistenceFault.ReasonType.TransientObject, e.Message).Throw();
                return true;
            }
            else if (e is UnresolvableObjectException)
            {
                new PersistenceFault(PersistenceFault.ReasonType.ObjectNotFound, e.Message).Throw();
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
