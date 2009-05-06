using System;
using System.Collections.Generic;

using System.Text;
using System.Reflection;
using Simple.Config;
using Simple.Common;
using Simple.Logging;
using log4net;
using Simple.ServiceModel;
using Simple.DynamicProxy;
using System.Diagnostics;

namespace Simple.Rules
{
    public class DefaultRulesProvider<T> : IRulesProvider<T> where T : class, ITestableService
    {
        protected static ILog Logger = MainLogger.Get(MethodInfo.GetCurrentMethod().DeclaringType);

        protected static Type ImplementerClass { get; set; }

        public DefaultRulesProvider(Assembly asm)
        {

            if (ImplementerClass == null)
                ImplementerClass = SearchForImplementerClass(asm);
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

        [DebuggerHidden]
        public T Create()
        {
            T obj = (T)Activator.CreateInstance(ImplementerClass);
            T obj1 = (T)RulesProxyBuilder.Instance.WrapInstance(obj, typeof(T));
            return (T)DynamicProxyFactory.Instance.CreateProxy(obj1, (o, m, p) =>
            {
                try
                {
                    SimpleContext.Get().Refresh(true);
                    return m.Invoke(o, p);
                }
                catch (TargetInvocationException e)
                {
                    Logger.Error("Error calling rules: " + e.Message, e);
                    throw ExHelper.ForReal(e);
                }
            });
        }

        public T CreateProxy(T obj)
        {
            return default(T);
        }
    }
}
