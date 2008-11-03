using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using BasicLibrary.Configuration;

namespace BasicLibrary.Cache
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

    public class MySection : ConfigElement
    {
        [ConfigElement("someIntValue", Required = true)]
        public int SomeIntValue { get; set; }

        [ConfigElement("someStringValue", Required = true)]
        public string SomeStringValue { get; set; }
    }
}
