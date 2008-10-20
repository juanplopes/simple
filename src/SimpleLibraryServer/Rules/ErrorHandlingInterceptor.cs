using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Core.Interceptor;
using SimpleLibrary.ServiceModel;
using BasicLibrary.Logging;

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
                if (!DefaultExceptionHandler.Handle(e)) {
                    throw;
                }
            }
        }

        #endregion
    }
}
