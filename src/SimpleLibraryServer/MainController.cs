using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using SimpleLibrary.ServiceModel;
using SimpleLibrary.DataAccess;
using BasicLibrary.ServiceModel;
using BasicLibrary.Logging;

namespace SimpleLibrary
{
    public class MainController
    {
        public static void Run(Assembly assembly)
        {
            MainLogger.Default.Info("Initializing server framework...");
            AssemblyLocatorHoster hoster = new AssemblyLocatorHoster();
            hoster.LocateServices(assembly);
            hoster.StartHosting();
        }
    }
}
