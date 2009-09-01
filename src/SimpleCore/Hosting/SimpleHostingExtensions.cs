using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Simple.Hosting;

namespace Simple
{
    public static class SimpleHostingExtensions
    {
        public static void InitServer(this Simply simply, Assembly asm)
        {
            InitServer(simply, asm, true);
        }

        public static void InitServer(this Simply simply, Assembly asm, bool wait)
        {
            simply.HostAssembly(asm);
            if (wait)
                new SimpleController().Wait();
        }
    }
}
