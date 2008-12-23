using System;
using System.Collections.Generic;

using System.Text;
using SimpleLibrary.ServiceModel;
using BasicLibrary.Logging;
using System.Reflection;
using SimpleLibrary.DataAccess;
using SimpleLibrary.Config;
using BasicLibrary.Configuration;
using log4net;
using System.Diagnostics;

namespace SimpleLibrary.Rules
{
    public class ErrorHandlingInterceptor
    {
        protected SimpleLibraryConfig Config = SimpleLibraryConfig.Get();
        ILog logger = MainLogger.Get(MethodInfo.GetCurrentMethod().DeclaringType);

        #region IInterceptor Members

        [DebuggerHidden]
        public object Interceptor(object target, MethodBase method, object[] parameters)
        {
            if (method.DeclaringType == typeof(object))
                return method.Invoke(target, parameters);

            try
            {
                logger.DebugFormat("Intercepting {1}.{0}...", method.Name, method.DeclaringType.Name);
                return method.Invoke(target, parameters);
            }
            catch (TargetInvocationException e)
            {
                if (!Handle(e.InnerException)) throw e.InnerException;
            }
            finally
            {
                if (SessionManager.IsInitialized)
                    SessionManager.ReleaseThreadSessions();
            }
            throw new InvalidProgramException("Cannot return without return");
        }

        [DebuggerHidden]
        protected bool Handle(Exception e)
        {
            foreach (IExceptionHandler handler in Config.Business.ExceptionHandling.GetHandlers())
            {
                if (handler.Handle(e)) return true;
            }

            return false;
        }

        #endregion
    }
}
