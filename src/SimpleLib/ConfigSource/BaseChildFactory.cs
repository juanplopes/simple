using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.ConfigSource
{
    public abstract class BaseChildFactory<T>
    {
        private ConfigRepository Repository { get; set; }
        public object Key { get; set; }

        public BaseChildFactory()
        {
            Key = ConfigSources.Default;
        }

        public void Init()
        {
            //lock (this)
            //{
            //    T config = ConfigSources.Get(Key).Get<T>();
            //    InitializeObjects(config);
            //    ConfigSources.Get(Key).AddExpiredHandler(
            //        t => InitializeObjects(t.Reload().Get()));
            //}
        }

        protected abstract void InitializeObjects(T config);

    }
}
