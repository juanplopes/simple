namespace Simple.Migrator.Framework.Loggers
{
	/// <summary>
	/// Handles writing a message to the log medium (i.e. file, console)
	/// </summary>
	public interface ILogWriter
	{
		/// <summary>
		/// Write this message
		/// </summary>
		/// <param name="message"></param>
		/// <param name="args"></param>
		void Write(string message, params object[] args);

		/// <summary>
		/// Write this message, as a line
		/// </summary>
		/// <param name="message"></param>
		/// <param name="args"></param>
		void WriteLine(string message, params object[] args);
	}
}
