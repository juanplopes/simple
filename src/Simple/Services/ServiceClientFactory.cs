using System;
using System.Collections.Generic;
using System.Linq;
using Simple.Config;
using Simple.Patterns;

namespace Simple.Services
{
    public class ServiceClientFactory : Factory<IServiceClientProvider>, Simple.Services.IServiceClientFactory
    {
        protected IList<Func<CallHookArgs, ICallHook>> CallHookCreators = new List<Func<CallHookArgs, ICallHook>>();
        protected IDictionary<Type, object> Replacements = new Dictionary<Type, object>();

        protected override void OnConfig(IServiceClientProvider config)
        {
        }
        protected override void OnClearConfig()
        {
            ConfigCache = new NullServiceClientProvider();
        }

        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        public object Resolve(Type type)
        {
            if (Replacements.ContainsKey(type)) return Replacements[type];

            return ConfigCache.ProxyObject(ConfigCache.Create(type), type, new DefaultInterceptor(GetHooks, ConfigCache.HeaderHandler, true));
        }

        #region IServiceClientFactory Members

        protected IEnumerable<ICallHook> GetHooks(CallHookArgs args)
        {
            foreach (var hook in CallHookCreators.Select(x=>x(args)))
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

        public DisposableAction BeginServiceMockContext(Type type, object service)
        {
            Replacements[type] = service;
            return new DisposableAction(() => Replacements.Remove(type));
        }

        #endregion
    }
}
