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
using Sample.Project.Database;

namespace Sample.Project.Tools.Database
{
    public class InsertDataCommand : ICommand
    {
        public bool ForceTestData { get; set; }

        #region ICommand Members

        public void Execute()
        {
            if (Configurator.IsProduction)
            {
                DataLists.All();
                if (ForceTestData)
                    DataLists.Test();
            }
            else
            {
                using (Context.Development)
                {
                    DataLists.All();
                    DataLists.Development();
                }

                using (Context.Test)
                {
                    DataLists.All();
                    DataLists.Test();
                }
            }
        }



        #endregion
    }
}
