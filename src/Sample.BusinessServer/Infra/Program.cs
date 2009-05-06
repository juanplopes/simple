
using System.Reflection;
using Simple;
using System.ComponentModel;
using Simple.ServiceModel;
using Simple.DataAccess;
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
