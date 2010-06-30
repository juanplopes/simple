using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Core;
using Simple.Generator;
using Sample.Project.Tools.Infra;
using Sample.Project.Environment;
using Simple;

namespace Sample.Project.Tools.Data
{
    public class InsertDataCommand : ICommand
    {
        public string Environment { get; set; }

        #region ICommand Members

        public void Execute()
        {
            var env = new Configurator(Environment ?? Simply.Do.GetGeneratorContext().Name);

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
