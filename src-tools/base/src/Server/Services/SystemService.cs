using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Patterns;
using NHibernate.Tool.hbm2ddl;
using Simple;
using System.ServiceProcess;
using Sample.Project.Domain;

namespace Sample.Project.Services
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
                    .Count(y => y.ServiceName == Simply.Do.GetConfig<ApplicationConfig>().Service.Name) == 1, "Not installed"));

            return new List<TaskRunner.Result>(runner.Results);
        }
    }
}
