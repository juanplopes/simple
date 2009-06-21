using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.ConfigSource
{
    public class NullConfigSource<T> : ConfigSource<T>
        where T : new()
    {
        public static NullConfigSource<T> Instance = new NullConfigSource<T>();
    }
}
