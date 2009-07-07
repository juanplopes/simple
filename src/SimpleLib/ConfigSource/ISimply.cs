﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.ConfigSource
{
    public interface ISimply<T>
    {
        void Configure(object key, IConfigSource<T> source);
    }
}
