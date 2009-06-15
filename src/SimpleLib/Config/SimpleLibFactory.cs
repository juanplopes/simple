using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.ConfigSource;
using Simple.Logging;

namespace Simple.Config
{
    public class SimpleLibFactory : 
        AggregateFactory<SimpleLibFactory>, 
        ILog4netFactory
    {
        Log4netFactory inner = null;

        public override void Configure()
        {
        }

        #region ILog4netFactory Members

        public log4net.ILog GetLogger(string name)
        {
            return inner.GetLogger(name);
        }

        public log4net.ILog GetLogger(Type type)
        {
            throw new NotImplementedException();
        }

        public log4net.ILog GetLogger(System.Reflection.MemberInfo member)
        {
            throw new NotImplementedException();
        }

        public log4net.ILog GetLogger(object obj)
        {
            throw new NotImplementedException();
        }

        public log4net.ILog GetLogger<T>()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
