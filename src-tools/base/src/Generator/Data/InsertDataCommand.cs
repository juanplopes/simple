using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Core;
using Simple.Generator;
using Sample.Project.Generator.Infra;
using Sample.Project.Environment;

namespace Sample.Project.Generator.Data
{
    public class InsertDataCommand : ICommand
    {
        public string Environment { get; set; }

        #region ICommand Members

        public void Execute()
        {
            var env = new Configurator(Environment ?? Program.Manager.Current.Name);


            if (env.IsDevelopment)
            {
                //DataManager.Get<ExampleData>().Execute();
                //development data here   
            }

            if (env.IsTest)
            {
                //unit test data here
            }

        }

        #endregion
    }
}
