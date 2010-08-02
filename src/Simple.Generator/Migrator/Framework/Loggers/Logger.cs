using System;
using System.Collections.Generic;

namespace Simple.Migrator.Framework.Loggers
{
	/// <summary>
	/// Text logger for the migration mediator
	/// </summary>
	public class Logger : IAttachableLogger
	{
		private readonly bool _trace;
		private readonly List<ILogWriter> _writers = new List<ILogWriter>();

		public Logger(bool trace)
		{
			_trace = trace;
		}

		public Logger(bool trace, params ILogWriter[] writers)
			: this(trace)
		{
			_writers.AddRange(writers);
		}

		public void Attach(ILogWriter writer)
		{
			_writers.Add(writer);
		}

		public void Detach(ILogWriter writer)
		{
			_writers.Remove(writer);
		}

		public void Started(long currentVersion, long finalVersion)
		{
			WriteLine("Current version : {0}.  Target version : {1}", currentVersion, finalVersion);
		}

		public void Started(List<long> currentVersions, long finalVersion)
		{
			WriteLine("Latest version applied : {0}.  Target version : {1}", LatestVersion(currentVersions), finalVersion);
		}

		public void MigrateUp(long version, string migrationName)
		{
			WriteLine("Applying {0}: {1}", version.ToString(), migrationName);
		}

		public void MigrateDown(long version, string migrationName)
		{
			WriteLine("Removing {0}: {1}", version.ToString(), migrationName);
		}

		public void Skipping(long version)
		{
			WriteLine("{0} {1}", version.ToString(), "<Migration not found>");
		}

		public void RollingBack(long originalVersion)
		{
			WriteLine("Rolling back to migration {0}", originalVersion);
		}

        public void ApplyingDBChange(string sql)
	    {
	        Log(sql);
	    }

		public void Exception(long version, string migrationName, Exception ex)
		{
            WriteLine("============ Error Detail ============");
            WriteLine("Error in migration: {0}", version);
            LogExceptionDetails(ex);
            WriteLine("======================================");
		}

        public void Exception(string message, Exception ex)
        {
            WriteLine("============ Error Detail ============");
            WriteLine("Error: {0}", message);
            LogExceptionDetails(ex);
            WriteLine("======================================");
        }

	    private void LogExceptionDetails(Exception ex)
	    {
	        WriteLine("{0}", ex.Message);
            WriteLine("{0}", ex.StackTrace);
	        Exception iex = ex.InnerException;
	        while (iex != null)
	        {
	            WriteLine("Caused by: {0}", iex);
                WriteLine("{0}", ex.StackTrace);
	            iex = iex.InnerException;
	        }
	    }

	    public void Finished(long originalVersion, long currentVersion)
		{
			WriteLine("Migrated to version {0}", currentVersion);
		}

		public void Finished(List<long> originalVersions, long currentVersion)
		{
			WriteLine("Migrated to version {0}", currentVersion);
		}

		public void Log(string format, params object[] args)
		{
			WriteLine(format, args);
		}

		public void Warn(string format, params object[] args)
		{
			Write("Warning! : ");
			WriteLine(format, args);
		}

		public void Trace(string format, params object[] args)
		{
			if (_trace)
			{
				Log(format, args);
			}
		}

		private void Write(string message, params object[] args)
		{
			foreach (ILogWriter writer in _writers)
			{
				writer.Write(message, args);
			}
		}

		private void WriteLine(string message, params object[] args)
		{
			foreach (ILogWriter writer in _writers)
			{
				writer.WriteLine(message, args);
			}
		}

		public static ILogger ConsoleLogger()
		{
			return new Logger(false, new ConsoleWriter());
		}
		
		private string LatestVersion(List<long> versions)
        {
			if (versions.Count > 0)
			{
				return versions[versions.Count - 1].ToString();
			}
			return "No migrations applied yet!";
		}
	}
}
