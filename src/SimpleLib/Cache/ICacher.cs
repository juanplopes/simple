using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
using System.Configuration;
using Simple.Configuration2;

namespace Simple.Cache
{
    public delegate void CacheExpired<T>(T identifier);

    public interface ICacher
    {
        bool Validate();
    }

    public interface ICacher<O> : ICacher
    {
        O GetValue();
    }

    public interface ICacher<T, O> : ICacher<O>
    {
        event CacheExpired<T> CacheExpiredEvent;
        T Identifier { get; }
    }
}
