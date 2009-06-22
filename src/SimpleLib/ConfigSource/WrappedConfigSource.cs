using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.ConfigSource
{
    public abstract class WrappedConfigSource<T1, T2> :
        ConfigSource<T1>,
        IWrappedConfigSource<T1, T2>
        where T1 : new()
        where T2 : new()
    {
        protected IConfigSource<T2> WrappedSource { get; set; }

        public IConfigSource<T1> Load(IConfigSource<T2> input)
        {
            lock (this)
            {
                SafeReset(input);
            }

            return this;
        }

        public abstract T1 TransformFromInput(T2 input);

        public void LoadAgain(IConfigSource<T2> input)
        {
            Load(input);
            InvokeReload();
        }

        public override bool Reload()
        {
            return true;
        }

        protected void SafeReset(IConfigSource<T2> source)
        {
            bool flag = false;

            if (WrappedSource != null)
            {
                WrappedSource.Reloaded -= SafeReload;
                flag = true;
            }

            WrappedSource = source;
            Cache = TransformFromInput(WrappedSource.Get());
            WrappedSource.Reloaded += SafeReload;

            if (flag) InvokeReload();
        }

        protected void SafeReload(T2 config)
        {
            Cache = TransformFromInput(config);
            InvokeReload();
        }
    }

    public class WrappedConfigSource<T> : WrappedConfigSource<T, T>, IWrappedConfigSource<T>
        where T:new()
    {
        public override T TransformFromInput(T input)
        {
            return input;
        }
    }
}
