using System;
using System.Collections.Generic;
using System.Text;
using Simple.ConfigSource;
using Simple.Patterns;

namespace Simple.Services
{
    public class ServiceClientFactory : Factory<IServiceClientProvider>, Simple.Services.IServiceClientFactory
    {
        protected IList<Func<CallHookArgs, ICallHook>> CallHookCreators { get; set; }

        protected override void OnConfig(IServiceClientProvider config)
        {
            ClearHooks();
        }




        protected override void OnClearConfig()
        {
            ConfigCache = new NullServiceClientProvider();
            ClearHooks();
        }

        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        public object Resolve(Type type)
        {
            return ConfigCache.ProxyObject(ConfigCache.Create(type), new DefaultInterceptor(GetHooks, ConfigCache.HeaderHandler, true));
        }

        #region IServiceClientFactory Members

        protected IEnumerable<ICallHook> GetHooks(CallHookArgs args)
        {
            foreach (var hook in Enumerable.Convert(CallHookCreators, x => x(args)))
            {
                if (hook != null) yield return hook;
            }
        }

        public void AddHook(Func<CallHookArgs, ICallHook> hookCreator)
        {
            CallHookCreators.Add(hookCreator);
        }

        public void ClearHooks()
        {
            CallHookCreators = new List<Func<CallHookArgs, ICallHook>>();
        }

        #endregion
    }
}
