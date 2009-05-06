
using System.Reflection;
using SimpleLibrary;
using System.ComponentModel;
using BasicLibrary.ServiceModel;
using SimpleLibrary.DataAccess;
using NHibernate.Tool.hbm2ddl;

namespace Sample.BusinessServer.Infra
{
    [RunInstaller(true)]
    public class Program : WCFWindowsServiceInstaller
    {
        public Program()
            : base("TestBusinessServer", "Test Business Server")
        {
            
        }

        public static void Main(string[] args)
        {
            MainController.Run(Assembly.GetExecutingAssembly());
        }
    }
}
