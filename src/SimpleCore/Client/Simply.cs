using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using Simple.Logging;
using System.Reflection;

namespace Simple.Client
{
    public static class Simply
    {
        #region Logger
        public static ILog Log(object obj)
        {
            return SimpleLogger.Get(obj);
        }

        public static ILog Log(string name)
        {
            return SimpleLogger.Get(name);
        }

        public static ILog Log(MemberInfo member)
        {
            return SimpleLogger.Get(member);
        }
        public static ILog Log<T>()
        {
            return SimpleLogger.Get<T>();
        }
        #endregion

    }
}
