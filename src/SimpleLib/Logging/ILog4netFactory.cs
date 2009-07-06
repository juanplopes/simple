using System;
namespace Simple.Logging
{
    public interface ILog4netFactory
    {
        log4net.ILog Log<T>();
        log4net.ILog Log(System.Reflection.MemberInfo member);
        log4net.ILog Log(object obj);
        log4net.ILog Log(string name);
        log4net.ILog Log(Type type);
    }
}
