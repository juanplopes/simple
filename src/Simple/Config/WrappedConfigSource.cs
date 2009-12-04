using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Config
{
    /// <summary>
    /// Wraps a config source inside another, making possible to convert a config source into another, 
    /// keeping autorefresh possibilities.
    /// </summary>
    /// <typeparam name="T1">The target type.</typeparam>
    /// <typeparam name="T2">The source type.</typeparam>
    public abstract class WrappedConfigSource<T1, T2> :
        ConfigSource<T1>,
        IWrappedConfigSource<T1, T2>
    {
        protected IConfigSource<T2> WrappedSource { get; set; }

        /// <summary>
        /// Loads the source config source (!) into this source (?).
        /// </summary>
        /// <param name="input">The config source.</param>
        /// <returns>The return point.</returns>
        public IConfigSource<T1> Load(IConfigSource<T2> input)
        {
            lock (this)
            {
                SafeReset(input);
            }

            return this;
        }

        /// <summary>
        /// When overriden in derived class, should implement transformation logic.
        /// </summary>
        /// <param name="input">The recently loaded config.</param>
        /// <returns>The transformated config.</returns>
        public abstract T1 TransformFromInput(T2 input);

        /// <summary>
        /// Loads another config source, invoking reload.
        /// </summary>
        /// <param name="input">The config source.</param>
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

    /// <summary>
    /// Wraps a config source inside another, making possible to convert a config source into another, 
    /// (of the same type) keeping autorefresh possibilities.
    /// </summary>
    /// <typeparam name="T1">The target type.</typeparam>
    /// <typeparam name="T2">The source type.</typeparam>
    public class WrappedConfigSource<T> : WrappedConfigSource<T, T>, IWrappedConfigSource<T>
    {
        /// <summary>
        /// By default, returns the same input as output.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The output.</returns>
        public override T TransformFromInput(T input)
        {
            return input;
        }
    }
}
