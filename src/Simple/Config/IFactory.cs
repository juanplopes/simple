
namespace Simple.Config
{
    public interface IFactory<T>
    {
        /// <summary>
        /// Indicates whether the factory has a config source associated to it.
        /// </summary>
        bool Initialized { get; }
        
        /// <summary>
        /// Initializes the factory using an instance of <see cref="IConfigSource<T>"/>.
        /// </summary>
        /// <param name="source">The source.</param>
        void Init(IConfigSource<T> source);

        /// <summary>
        /// Clears the configuration.
        /// </summary>
        void Clear();
    }
}
