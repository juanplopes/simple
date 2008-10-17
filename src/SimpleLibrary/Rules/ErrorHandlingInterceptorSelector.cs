using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;

namespace SimpleLibrary.Rules
{
    public class ErrorHandlingInterceptorSelector : IInterceptorSelector
    {
        public Castle.Core.Interceptor.IInterceptor[] SelectInterceptors(Type type, System.Reflection.MethodInfo method, Castle.Core.Interceptor.IInterceptor[] interceptors)
        {
            return interceptors;
        }
    }
}
