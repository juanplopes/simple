using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Patterns;
using NHibernate.Tool.hbm2ddl;
using Simple;
using System.ServiceProcess;
using Example.Project.Domain;
using Simple.Services;

namespace Example.Project.Services
{
    public class SystemService : MarshalByRefObject, ISystemService
    {
        public IList<TaskRunner.Result> Check()
        {
            TaskRunner runner = new TaskRunner();

            runner.Run("Database mapping", "Correctly mapped", x =>
                new SchemaValidator(Simply.Do.GetNHibernateConfig()).Validate());

            runner.Run("Windows Service installed", "Installed",
                x => x.WarnUnless(ServiceController.GetServices()
                    .Any(y => y.ServiceName == Simply.Do.GetConfig<ApplicationConfig>().Service.Name), "Not installed"));


            return new List<TaskRunner.Result>(runner.Results);
        }

        public int Test(int value)
        {
            return 0;
        }
    }
}
