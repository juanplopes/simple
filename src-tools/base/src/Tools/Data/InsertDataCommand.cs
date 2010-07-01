using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using Sample.Project.Tools.Infra;
using Sample.Project.Environment;
using Simple;
using Sample.Project.Tools.Data.Development;
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

            DataManager.Get<ExampleData>().Execute();

            if (!Configurator.IsProduction)
            {
                using (Context.Development)
                {
                    
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
