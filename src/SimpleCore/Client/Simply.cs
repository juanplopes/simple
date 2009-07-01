using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using Simple.Logging;
using System.Reflection;
using Simple.Services;

namespace Simple.Client
{
    public class Simply
    {
        #region Logger
        public static ILog Log(object obj)
        {
            return LoggerManager.Get(obj);
        }
        public static ILog Log(string name)
        {
            return LoggerManager.Get(name);
        }
        public static ILog Log(MemberInfo member)
        {
            return LoggerManager.Get(member);
        }
        public static ILog Log(Type type)
        {
            return LoggerManager.Get(type);
        }
        public static ILog Log<T>()
        {
            return LoggerManager.Get<T>();
        }
        #endregion

        #region Services
        public static void Host(Type type)
        {
            ServiceManager.Host(type);
        }
        public static void Host(object key, Type type)
        {
            ServiceManager.Host(key, type);
        }
        public static T Connect<T>()
        {
            return ServiceManager.Connect<T>();
        }
        public static T Connect<T>(object key)
        {
            return ServiceManager.Connect<T>(key);
        }
        #endregion
    }
}
