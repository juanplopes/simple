using System;
using System.Collections.Generic;

using System.Text;
using System.Reflection;
using SimpleLibrary.ServiceModel;
using SimpleLibrary.DataAccess;
using BasicLibrary.ServiceModel;
using BasicLibrary.Logging;
using log4net;
using System.Diagnostics;

namespace SimpleLibrary
{
    public class MainController
    {
        protected static ILog Logger = MainLogger.Get(MethodInfo.GetCurrentMethod().DeclaringType);

        public static void Run(Assembly assembly)
        {
            Logger.Info("Initializing server framework...");
            AssemblyLocatorHoster hoster = new AssemblyLocatorHoster();
            hoster.LocateServices(assembly);
            hoster.StartHosting();
        }
    }
}
