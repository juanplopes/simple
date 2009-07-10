using System;
using System.Collections.Generic;
using System.Linq;


using System.Text;
using Simple.Patterns;

namespace Simple.ConfigSource
{
    public delegate void ConfigReloadedDelegate<T>(T config);

    public interface IConfigSource<T> : IDisposable
    {
        bool Loaded { get; }
        T Get();
        void AddTransform(Func<T, T> func);
        bool Reload();
        event ConfigReloadedDelegate<T> Reloaded;

    }

    public interface IFileConfigSource<T> : IConfigSource<T>
    {
        IConfigSource<T> LoadFile(string fileName);
    }


    public interface IConfigSource<T, A> : IConfigSource<T>
    {
        IConfigSource<T> Load(A input);
    }
}

