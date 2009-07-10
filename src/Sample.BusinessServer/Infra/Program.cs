using System;
using System.Collections.Generic;

using System.Text;
using SimpleLibrary.ServiceModel;
using System.Reflection;
using SimpleLibrary;
using System.ComponentModel;
using BasicLibrary.ServiceModel;
using Sample.BusinessServer.Rules;
using SimpleLibrary.Filters;
using Sample.BusinessInterface.Domain;
using Sample.BusinessServer.DataAccess;
using NHibernate.Tool.hbm2ddl;
using SimpleLibrary.DataAccess;
using SimpleLibrary.Threading;
using System.Threading;
using NHibernate;
using BasicLibrary.Logging;
using System.Globalization;
using Sample.BusinessInterface.Rules;
using SimpleLibrary.Rules;

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
