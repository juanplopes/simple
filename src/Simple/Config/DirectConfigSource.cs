using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Patterns;

namespace Simple.Config
{
    /// <summary>
    /// Configuration source that only loads an instance of the config type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DirectConfigSource<T> : ConfigSource<T>, IConfigSource<T,T>
    {
        #region IConfigSource<T,T> Members

        /// <summary>
        /// Loads the instance.
        /// </summary>
        /// <param name="input">The instance.</param>
        /// <returns>The return point.</returns>
        public IConfigSource<T> Load(T input)
        {
            Cache = input;
            return this;
        }

        #endregion
    }
}
