using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Reflection;

namespace Simple.Logging
{
    public interface ILog4netFactory
    {
        ILog GetLogger(string name);
        ILog GetLogger(Type type);
        ILog GetLogger(MemberInfo member);
        ILog GetLogger(object obj);
        ILog GetLogger<T>();
    }
}
