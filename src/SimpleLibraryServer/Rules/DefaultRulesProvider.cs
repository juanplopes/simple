using System;
using System.Collections.Generic;

using System.Text;
using System.Reflection;
using SimpleLibrary.Config;
using Castle.DynamicProxy;
using BasicLibrary.Common;
using Castle.Core.Interceptor;
using BasicLibrary.Logging;
using log4net;

namespace SimpleLibrary.Rules
{
    public class DefaultRulesProvider<T> : IRulesProvider<T> where T : class
    {
        protected static ILog Logger = MainLogger.Get(MethodInfo.GetCurrentMethod().DeclaringType);

        protected static Type ImplementerClass { get; set; }
        protected static Type ProxyClass { get; set; }

        public DefaultRulesProvider(Assembly asm)
        {

            if (ImplementerClass == null)
                ImplementerClass = SearchForImplementerClass(asm);
            if (ProxyClass == null)
                ProxyClass = RulesProxyBuilder.CreateProxy(ImplementerClass, typeof(T));
        }

        public DefaultRulesProvider() : this(SimpleLibraryConfig.Get().Business.ServerAssembly.LoadAssembly())
        {

        }

        protected Type SearchForImplementerClass(Assembly asm)
        {
            SimpleLibraryConfig config = SimpleLibraryConfig.Get();

            Logger.Debug("Searching for implementation for " + typeof(T).Name + " in " + asm.FullName);
            foreach (Type t in asm.GetTypes())
            {
                if (typeof(T).IsAssignableFrom(t) && t.GetConstructor(Type.EmptyTypes) != null)
                {
                    Logger.Debug("Found " + t.FullName + ".");
                    return t;
                }
            }
            throw new InvalidOperationException("Could not find implementation for interface " + typeof(T).FullName);
        }

        public T Create()
        {
            return (T)RulesProxyBuilder.CreateInstanceFromProxyType(ProxyClass);
        }

        public T CreateProxy(T obj)
        {
            return default(T);
        }
    }
}
