using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Simple.Tests
{
    public class Server
    {
        static int Main(string[] args)
        {
            MainController.Run(Assembly.GetExecutingAssembly());
            return 0;
        }
    }
}
