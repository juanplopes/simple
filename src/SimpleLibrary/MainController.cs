using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using SimpleLibrary.ServiceModel;
using SimpleLibrary.DataAccess;
using BasicLibrary.ServiceModel;

namespace SimpleLibrary
{
    public class MainController
    {
        public static void Run(Assembly assembly)
        {
            AssemblyLocatorHoster hoster = new AssemblyLocatorHoster();
            hoster.LocateServices(assembly);
            hoster.StartHosting();
        }
    }
}
