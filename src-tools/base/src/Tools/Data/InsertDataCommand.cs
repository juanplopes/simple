using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using Sample.Project.Tools.Infra;
using Sample.Project.Environment;
using Simple;
using Simple.Generator.Data;
using Simple.Patterns;

namespace Sample.Project.Tools.Data
{
    public class InsertDataCommand : ICommand
    {
        public string Environment { get; set; }


        #region ICommand Members

        public void Execute()
        {
            //here, samples that will run in all environments, even production
            //DataManager.Get<ExampleData>().Execute();

            if (!Configurator.IsProduction)
            {
                using (Context.Development)
                {
                    //here, development sample data
                }

                using (Context.Test)
                {
                    //unit test data here
                }
            }
        }




        #endregion
    }
}
