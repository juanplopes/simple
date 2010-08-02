using System;

namespace Simple.Migrator.Framework
{
	/// <summary>
	/// Base class for migration errors.
	/// </summary>
	public class MigrationException : Exception
	{
	    public MigrationException(string message)
			: base(message) {}
			
		public MigrationException(string message, Exception cause)
			: base(message, cause) {}
			
		public MigrationException(string migration, int version, Exception innerException)
			: base(String.Format("Exception in migration {0} (#{1})", migration, version), innerException) {}
	}
}
