using System;
using System.Collections.Generic;
using System.Linq;
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
            //SchemaExport export = new SchemaExport(SessionManager.Config);
            //export.Execute(false, true, false, true);

            ISession session1 = SessionManager.GetSession(true);
            ISession session2 = SessionManager.GetSession(true);

            FuncionarioDao fd1 = new FuncionarioDao(session1);
            FuncionarioDao fd2 = new FuncionarioDao(session2);

            Funcionario e = fd1.Load(1);
            e.Nome = DateTime.Now.ToString();
            fd2.Update(e);
            session2.Flush();

            TestPersistedState test = TestPersistedState.Get(10, 3);

            Thread t = new Thread(new ThreadStart(delegate()
            {
                TestPersistedState test2 = TestPersistedState.Get(10, 3);
            }));
            t.Start();

            test.Ola = "olálálá";
            Thread.Sleep(100000);
            test.Persist();

            MainController.Run(Assembly.GetExecutingAssembly());
        }
    }
}
