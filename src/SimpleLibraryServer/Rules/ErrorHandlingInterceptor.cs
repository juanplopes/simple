using System;
using System.Collections.Generic;

using System.Text;
using Castle.Core.Interceptor;
using SimpleLibrary.ServiceModel;
using BasicLibrary.Logging;
using System.Reflection;
using SimpleLibrary.DataAccess;
using SimpleLibrary.Config;
using BasicLibrary.Configuration;

namespace SimpleLibrary.Rules
{
    public class ErrorHandlingInterceptor : IInterceptor
    {
        SimpleLibraryConfig Config = SimpleLibraryConfig.Get();

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

            foreach (IExceptionHandler handler in Config.Business.ExceptionHandling.GetHandlers())
            {
                if (handler.Handle(e)) return true;
            }

            return false;
        }

        #endregion
    }
}
