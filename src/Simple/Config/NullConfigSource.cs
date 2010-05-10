
namespace Simple.Config
{
    /// <summary>
    /// Null values returning configuration source.
    /// </summary>
    /// <typeparam name="T">The config type.</typeparam>
    public class NullConfigSource<T> : ConfigSource<T>
    {
        /// <summary>
        /// Singleton instance of the class.
        /// </summary>
        public static NullConfigSource<T> Instance = new NullConfigSource<T>();
    }
}
