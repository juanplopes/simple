using System;
using System.Collections.Generic;
using System.Linq;


using System.Text;

namespace Simple.ConfigSource
{
    public delegate void ConfigReloadedDelegate<T>(T config);

    public interface IConfigSource<T> : IDisposable
    {
        bool Loaded { get; }
        T Get();
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
 
