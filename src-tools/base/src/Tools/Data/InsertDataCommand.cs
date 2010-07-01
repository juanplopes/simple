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
using Sample.Project.Tools.Data.Development;

namespace Sample.Project.Tools.Data
{
    public class InsertDataCommand : ICommand
    {
        #region ICommand Members

        public void Execute()
        {
            //here, samples that will run in all environments, even production
            

            if (!Configurator.IsProduction)
            {
                using (Context.Development)
                {
                    //here, development sample data
                    //DataManager.Get<ExampleData>().Execute();
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
