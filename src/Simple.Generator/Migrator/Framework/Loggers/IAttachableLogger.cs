namespace Simple.Migrator.Framework.Loggers
{
	/// <summary>
	/// ILogger interface. 
	/// Implicit in this interface is that the logger will delegate actual
	/// logging to the <see cref="ILogWriter"/>(s) that have been attached
	/// </summary>
	public interface IAttachableLogger: ILogger
	{
		/// <summary>
		/// Attach an <see cref="ILogWriter"/>
		/// </summary>
		/// <param name="writer"></param>
		void Attach(ILogWriter writer);

		/// <summary>
		/// Detach an <see cref="ILogWriter"/>
		/// </summary>
		/// <param name="writer"></param>
		void Detach(ILogWriter writer);
	}
}
