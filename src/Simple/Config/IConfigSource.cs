using System;



namespace Simple.Config
{
    /// <summary>
    /// Defines signature to methods used to handle config reloads.
    /// </summary>
    /// <typeparam name="T">The config type.</typeparam>
    /// <param name="config">The just reloaded config.</param>
    public delegate void ConfigReloadedDelegate<T>(T config);

    /// <summary>
    /// Defines the contract for configuration source classes based on <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IConfigSource<T> : IDisposable
    {
        /// <summary>
        /// Gets whether the class is loaded or not.
        /// </summary>
        bool Loaded { get; }

        /// <summary>
        /// Gets the last loaded configuration.
        /// </summary>
        /// <returns>The configuration.</returns>
        T Get();

        /// <summary>
        /// Adds a transformation function.
        /// </summary>
        /// <param name="func">The function.</param>
        /// <returns>The return point.</returns>
        IConfigSource<T> AddTransform(Func<T, T> func);

        /// <summary>
        /// Adds a transformation function.
        /// </summary>
        /// <param name="func">The function.</param>
        /// <returns>The return point.</returns>
        IConfigSource<T> AddTransform(Action<T> func);

        /// <summary>
        /// Forces config reload.
        /// </summary>
        /// <returns>True if the reload was ok, false otherwise.</returns>
        bool Reload();

        /// <summary>
        /// Event describing when this config source has expired.
        /// </summary>
        event ConfigReloadedDelegate<T> Reloaded;

    }

    /// <summary>
    /// Specific contracts for xml file config sources.
    /// </summary>
    /// <typeparam name="T">The config type.</typeparam>
    public interface IXmlFileConfigSource<T> : IConfigSource<T>
    {
        /// <summary>
        /// Loads the file in <paramref name="fileName"/>.
        /// </summary>
        /// <param name="fileName">The filename.</param>
        /// <returns>Return point.</returns>
        IConfigSource<T> LoadFile(string fileName);

        /// <summary>
        /// Loads the file in <paramref name="fileName"/> and XPath.
        /// </summary>
        /// <param name="fileName">The filename.</param>
        /// <param name="xPath">The XPath.</param>
        /// <returns>Return point.</returns>
        IConfigSource<T> LoadFile(string fileName, string xPath);
    }

    /// <summary>
    /// Defines a generic interface for loading config sources.
    /// </summary>
    /// <typeparam name="T">The config type.</typeparam>
    /// <typeparam name="A">The input type.</typeparam>
    public interface IConfigSource<T, A> : IConfigSource<T>
    {
        /// <summary>
        /// Loads the input into the source, monitoring it, when avaliable.
        /// </summary>
        /// <param name="input">The input to be loaded</param>
        /// <returns>The return point.</returns>
        IConfigSource<T> Load(A input);
    }
}

