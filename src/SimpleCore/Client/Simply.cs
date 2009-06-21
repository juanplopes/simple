using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using Simple.Logging;
using System.Reflection;

namespace Simple.Client
{
    public class Simply
    {
        #region Logger
        public static ILog Log(object obj)
        {
            return Logger.Get(obj);
        }
        public static ILog Log(string name)
        {
            return Logger.Get(name);
        }
        public static ILog Log(MemberInfo member)
        {
            return Logger.Get(member);
        }
        public static ILog Log(Type type)
        {
            return Logger.Get(type);
        }
        public static ILog Log<T>()
        {
            return Logger.Get<T>();
        }
        #endregion

    }
}
