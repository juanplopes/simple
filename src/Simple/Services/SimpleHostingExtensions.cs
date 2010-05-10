using System;
using System.Reflection;
using Simple.Hosting;

namespace Simple
{
    public static class SimpleHostingExtensions
    {
        public static void InitServer<T>(this Simply simply)
        {
            simply.InitServer(typeof(T));
        }

        public static void InitServer(this Simply simply, Type type)
        {
            simply.InitServer(type.Assembly);
        }

        public static void InitServer(this Simply simply, Assembly asm)
        {
            simply.HostAssembly(asm);
        }

        public static void WaitRequests(this Simply simply)
        {
            new SimpleController().Wait();
        }
    }
}
