using System;
using System.Collections.Generic;

using System.Text;
using log4net;
using System.Reflection;
using BasicLibrary.Logging;
using log4net.Core;
using System.Diagnostics;



namespace BasicLibrary.Cache
{
    public abstract class BaseCacher<T, O> : ICacher<T, O>
    {
        protected static ILog Logger = MainLogger.Get(MethodInfo.GetCurrentMethod().DeclaringType);

        public static string sTrying = "Trying to get cached value...";
        public static string sNotValid = "Stored cached value was not valid. Reloading cache...";
        public static string sReturningCached = "Returning cached value...";

        public abstract event CacheExpired<T> CacheExpiredEvent;

        public T Identifier { get; protected set; }
        public abstract bool Validate();
        public abstract O GetValue();

        protected void Log(string message)
        {
            Debug.Assert(message != null);

            if (Logger != null)
                Logger.DebugFormat("{1}: " + message, this.GetType().Name, GetFormattedId());
        }

        protected virtual string GetFormattedId()
        {
            Debug.Assert(Identifier != null);

            return Identifier.ToString();
        }
    }
}
