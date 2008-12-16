using System;
using System.Collections.Generic;

using System.Text;
using SimpleLibrary.ServiceModel;
using BasicLibrary.Logging;
using System.Reflection;
using SimpleLibrary.DataAccess;
using SimpleLibrary.Config;
using BasicLibrary.Configuration;

namespace SimpleLibrary.Rules
{
    public class ErrorHandlingInterceptor
    {
        SimpleLibraryConfig Config = SimpleLibraryConfig.Get();

        #region IInterceptor Members

        public object Interceptor(object target, MethodBase method, object[] parameters)
        {
            try
            {
                return method.Invoke(target, parameters);
            }
            catch (Exception e)
            {
                if (!Handle(e)) throw;
            }
            finally
            {
                if (SessionManager.IsInitialized)
                    SessionManager.ReleaseThreadSessions();
            }
            throw new InvalidProgramException("Cannot return without return");
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
