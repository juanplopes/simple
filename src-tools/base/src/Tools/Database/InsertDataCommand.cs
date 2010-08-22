using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using Example.Project.Tools.Infra;
using Example.Project.Config;
using Simple;
using Simple.Generator.Data;
using Simple.Patterns;
using Example.Project.Database;

namespace Example.Project.Tools.Database
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
