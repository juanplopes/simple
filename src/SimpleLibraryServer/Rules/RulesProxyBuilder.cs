using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using Castle.Core.Interceptor;
using System.Reflection;
using BasicLibrary.Logging;

namespace SimpleLibrary.Rules
{
    public class RulesProxyBuilder
    {
        public static Type CreateProxy(Type ruleType, Type interfaceType)
        {
            MainLogger.Default.Debug("Creating proxy for type " + ruleType.FullName + "...");
            DefaultProxyBuilder builder = new DefaultProxyBuilder();
            ProxyGenerationOptions options = new ProxyGenerationOptions(new ErrorHandlingGenerationHook());
            return builder.CreateClassProxy(ruleType, new Type[] { interfaceType }, options);
        }

        public static object CreateInstanceFromProxyType(Type type)
        {
            MainLogger.Default.Debug("Creating proxy instance. Proxy type: " + type.FullName);
            return Activator.CreateInstance(type, new object[] { 
                new IInterceptor[] { new ErrorHandlingInterceptor() } });
        }

        public static object CreateInstance(Type ruleType, Type interfaceType)
        {
            Type type = CreateProxy(ruleType, interfaceType);
            return CreateInstanceFromProxyType(type);
        }
    }
}
