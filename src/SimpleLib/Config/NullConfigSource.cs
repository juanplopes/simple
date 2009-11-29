using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Config
{
    public class NullConfigSource<T> : ConfigSource<T>
    {
        public static NullConfigSource<T> Instance = new NullConfigSource<T>();
    }
}
