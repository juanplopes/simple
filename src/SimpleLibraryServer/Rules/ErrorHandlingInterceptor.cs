using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Core.Interceptor;
using SimpleLibrary.ServiceModel;
using BasicLibrary.Logging;
using System.Reflection;
using SimpleLibrary.DataAccess;

namespace SimpleLibrary.Rules
{
    public class ErrorHandlingInterceptor : IInterceptor
    {
        #region IInterceptor Members

        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (Exception e)
            {
                if (!Handle(e)) throw;
            }
            finally
            {
                SessionManager.ReleaseThreadSessions();
            }
        }

        protected bool Handle(Exception e)
        {
            if (e is TargetInvocationException) e = e.InnerException;

            return !DefaultExceptionHandler.Handle(e);
        }

        #endregion
    }
}
