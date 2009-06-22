using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Core;
using Simple.Common;
using Simple.Patterns;

namespace Simple.Logging
{
    public class NullLogger : ILogger
    {
        #region ILogger Members

        public bool IsEnabledFor(Level level)
        {
            return false;
        }

        public void Log(LoggingEvent logEvent)
        {

        }

        public void Log(Type callerStackBoundaryDeclaringType, Level level, object message, Exception exception)
        {

        }

        public string Name
        {
            get { return string.Empty; }
        }

        public log4net.Repository.ILoggerRepository Repository
        {
            get { return null; }
        }

        #endregion
    }


    public class NullLog : ILog
    {
        #region ILog Members

        public void Debug(object message, Exception exception)       {       }
        public void Debug(object message)
        {
            
        }

        public void DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            
        }

        public void DebugFormat(string format, object arg0, object arg1, object arg2)
        {
            
        }

        public void DebugFormat(string format, object arg0, object arg1)
        {
            
        }

        public void DebugFormat(string format, object arg0)
        {
            
        }

        public void DebugFormat(string format, params object[] args)
        {
            
        }

        public void Error(object message, Exception exception)
        {
            
        }

        public void Error(object message)
        {
            
        }

        public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            
        }

        public void ErrorFormat(string format, object arg0, object arg1, object arg2)
        {
            
        }

        public void ErrorFormat(string format, object arg0, object arg1)
        {
            
        }

        public void ErrorFormat(string format, object arg0)
        {
            
        }

        public void ErrorFormat(string format, params object[] args)
        {
            
        }

        public void Fatal(object message, Exception exception)
        {
            
        }

        public void Fatal(object message)
        {
            
        }

        public void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            
        }

        public void FatalFormat(string format, object arg0, object arg1, object arg2)
        {
            
        }

        public void FatalFormat(string format, object arg0, object arg1)
        {
            
        }

        public void FatalFormat(string format, object arg0)
        {
            
        }

        public void FatalFormat(string format, params object[] args)
        {
            
        }

        public void Info(object message, Exception exception)
        {
            
        }

        public void Info(object message)
        {
            
        }

        public void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            
        }

        public void InfoFormat(string format, object arg0, object arg1, object arg2)
        {
            
        }

        public void InfoFormat(string format, object arg0, object arg1)
        {
            
        }

        public void InfoFormat(string format, object arg0)
        {
            
        }

        public void InfoFormat(string format, params object[] args)
        {
            
        }

        public bool IsDebugEnabled
        {
            get { return false; }
        }

        public bool IsErrorEnabled
        {
            get { return false; }
        }

        public bool IsFatalEnabled
        {
            get { return false; }
        }

        public bool IsInfoEnabled
        {
            get { return false; }
        }

        public bool IsWarnEnabled
        {
            get { return false; }
        }

        public void Warn(object message, Exception exception)
        {
            
        }

        public void Warn(object message)
        {
            
        }

        public void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            
        }

        public void WarnFormat(string format, object arg0, object arg1, object arg2)
        {
            
        }

        public void WarnFormat(string format, object arg0, object arg1)
        {
            
        }

        public void WarnFormat(string format, object arg0)
        {
            
        }

        public void WarnFormat(string format, params object[] args)
        {
            
        }

        #endregion

        #region ILoggerWrapper Members

        public log4net.Core.ILogger Logger
        {
            get { return Singleton<NullLogger>.Instance; }
        }

        #endregion
    }
}
