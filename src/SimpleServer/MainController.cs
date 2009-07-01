
using System.Reflection;
using Simple.ServiceModel;
using Simple.Logging;
using log4net;

namespace Simple
{
    public class MainController
    {
        protected static ILog Logger = LoggerManager.Get(MethodInfo.GetCurrentMethod().DeclaringType);

        public static void Run(Assembly assembly)
        {
            Logger.Info("Initializing server framework...");
            AssemblyLocatorHoster hoster = new AssemblyLocatorHoster();
            hoster.LocateServices(assembly);
            hoster.StartHosting();
        }
    }
}
