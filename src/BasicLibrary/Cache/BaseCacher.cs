using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicLibrary.Logging;

namespace BasicLibrary.Cache
{
    public abstract class BaseCacher<T, O> : ICacher<T, O>
    {
        public static string sTrying = "Trying to get cached value...";
        public static string sNotValid = "Stored cached value was not valid. Reloading cache...";
        public static string sReturningCached = "Returning cached value...";
        
        public abstract event CacheExpired<T> CacheExpiredEvent;

        public T Identifier { get; protected set; }
        public abstract bool Validate();
        public abstract O GetValue();

        public void Log(string message)
        {
            MainLogger.Default.DebugFormat("{1}: " + message, this.GetType().Name, GetFormattedId());
        }

        public virtual string GetFormattedId()
        {
            return Identifier.ToString();
        }
    }
}
