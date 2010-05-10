
namespace Simple.Config
{
    /// <summary>
    /// Defines a contract for using a config source as an input for other config source.
    /// </summary>
    /// <typeparam name="T">The target config type.</typeparam>
    /// <typeparam name="A">The source config type.</typeparam>
    public interface IWrappedConfigSource<T, A> : IConfigSource<T, IConfigSource<A>>
    {
    }

    /// <summary>
    /// Defines a contract for using a config source as an input for other config source of same type.
    /// </summary>
    /// <typeparam name="T">The config type.</typeparam>
    public interface IWrappedConfigSource<T> : IWrappedConfigSource<T, T>
        { }
}
