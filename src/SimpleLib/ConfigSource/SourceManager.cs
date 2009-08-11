using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Logging;
using Simple.Patterns;

namespace Simple.ConfigSource
{
    internal class SourceFor<C> : Singleton<SourceFor<C>>
    {
        private IDictionary<object, WrappedConfigSource<C>> Configs =
            new Dictionary<object, WrappedConfigSource<C>>();

        public WrappedConfigSource<C> Get(object key)
        {
            lock (Configs)
            {
                if (key == null) key = SourceManager.Do.DefaultKey;

                WrappedConfigSource<C> ret = null;

                if (!Configs.TryGetValue(key, out ret))
                {
                    ret = new WrappedConfigSource<C>().Load(NullConfigSource<C>.Instance)
                        as WrappedConfigSource<C>;


                    Configs[key] = ret;
                }

                return ret;
            }
        }
        public void Set(object key, IConfigSource<C> source)
        {
            lock (Configs)
            {
                var wrapped = Get(key);
                wrapped.Load(source);
            }
        }

        public void Clear()
        {
            lock (Configs)
            {
                foreach (var item in Configs.Keys)
                {
                    Set(item, NullConfigSource<C>.Instance);
                }
            }
        }

    }

    public class SourceManager : Singleton<SourceManager>
    {
        public object DefaultKey = new object();

        public IConfigSource<C> Get<C>()
        {
            return Get<C>(null);
        }

        public IConfigSource<C> Get<C>(object key)
        {
            return SourceFor<C>.Do.Get(key);
        }

        public void Register<C>(IConfigSource<C> source)
        {
            Register(null, source);
        }
        public void Register<C>(object key, IConfigSource<C> source)
        {
            SourceFor<C>.Do.Set(key, source);
        }

        public void Remove<C>()
        {
            Remove<C>(null);
        }
        public void Remove<C>(object key)
        {
            SourceFor<C>.Do.Set(key, NullConfigSource<C>.Instance);
        }

        public void Clear<C>()
        {
            SourceFor<C>.Do.Clear();
        }

        public void AttachFactory<C>(IFactory<C> factory)
        {
            AttachFactory(null, factory);
        }
        public void AttachFactory<C>(object key, IFactory<C> factory)
        {
            factory.Init(SourceFor<C>.Do.Get(key));
        }

        public object BestKeyOf(params object[] keys)
        {
            object ret = null;
            foreach (object obj in keys)
            {
                ret = obj;
                if (ret != null && ret != SourceManager.Do.DefaultKey) break;
            }
            return ret;
        }
    }
}
