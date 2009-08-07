using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Patterns;

namespace Simple.ConfigSource
{
    public class DirectConfigSource<T> : ConfigSource<T>, IConfigSource<T,T>
    {
        #region IConfigSource<T,T> Members

        public IConfigSource<T> Load(T input)
        {
            Cache = input;
            return this;
        }

        #endregion
    }
}
