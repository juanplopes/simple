using System;
using System.Collections.Generic;

using System.Text;
using Castle.DynamicProxy;
using Castle.Core.Interceptor;
using System.Reflection;
using BasicLibrary.Logging;
using log4net;
using Cramon.NetExtension.DynamicProxy;

namespace SimpleLibrary.Rules
{
    public class RulesProxyBuilder
    {
        public static RulesProxyBuilder Instance { get { return Nested.Builder; } }
        protected class Nested
        {
            public static RulesProxyBuilder Builder = new RulesProxyBuilder();
        }

        protected ErrorHandlingInterceptor _interceptor = new ErrorHandlingInterceptor();
        protected ILog Logger = MainLogger.Get(MethodInfo.GetCurrentMethod().DeclaringType);

        public object WrapInstance(object obj, Type interfaceType)
        {
            DynamicProxyFactory factory = DynamicProxyFactory.Instance;
            return factory.CreateProxy(obj, new InvocationDelegate(_interceptor.Interceptor), true, new Type[] { interfaceType });
        }
    }
}
