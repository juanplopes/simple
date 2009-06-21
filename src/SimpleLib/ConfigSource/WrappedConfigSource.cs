using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.ConfigSource
{
    public class WrappedConfigSource<T> : 
        ConfigSource<T>,
        IConfigSource<T, IConfigSource<T>>
        where T : new()
    {
        protected IConfigSource<T> WrappedSource { get; set; }

        public IConfigSource<T> Load(IConfigSource<T> input)
        {
            if (WrappedSource != null) WrappedSource.Reloaded -= SafeReload;

            WrappedSource = input;

            Cache = input.Get();
            input.Reloaded += SafeReload;

            return this;
        }

        public WrappedConfigSource<T> LoadSelf(IConfigSource<T> input)
        {
            return Load(input) as WrappedConfigSource<T>;
        }

        public void LoadAgain(IConfigSource<T> input)
        {
            LoadSelf(input).InvokeReload();
        }

        public override bool Reload()
        {
            return true;
        }

        protected void SafeReload(T config)
        {
            Cache = config;
            InvokeReload();
        }
    }
}
