using System;
using System.Collections.Generic;

using System.Text;
using Simple.ServiceModel;
using Simple.Logging;
using System.Reflection;
using Simple.DataAccess;
using Simple.Config;
using Simple.Configuration2;
using log4net;
using System.Diagnostics;
using Simple.Common;
using log4net.Repository.Hierarchy;

namespace Simple.Rules
{
    public class ErrorHandlingInterceptor
    {
        protected SimpleConfig Config = SimpleConfig.Get();
        ILog logger = SimpleLogger.Get(MethodInfo.GetCurrentMethod().DeclaringType);

        #region IInterceptor Members


        [DebuggerNonUserCode]
        [DebuggerStepThrough]
        [DebuggerStepperBoundary]
        public object Interceptor(object target, MethodBase method, object[] parameters)
        {
            if (method.DeclaringType == typeof(object))
                return method.Invoke(target, parameters);

            using (DataContext.Enter())
            {
                try
                {
                    logger.DebugFormat("Intercepting {1}.{0}...", method.Name, method.DeclaringType.Name);
                    return method.Invoke(target, parameters);
                }
                catch (Exception e)
                {
                    e = ExceptionHelper.ForReal(e);
                    SimpleLogger.Get(this).Error(
                        string.Format("There was an error inside a rule: {0}", e.Message), e);
                    if (!Handle(e)) throw e;

                }
            }
            throw new InvalidProgramException("Cannot return without return");
        }

        [DebuggerNonUserCode]
        [DebuggerStepThrough]
        [DebuggerStepperBoundary]
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
