using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using Simple;
using Simple.Generator.Data;
using Simple.Patterns;
using Example.Project.Tests.Fixtures;

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
                FixtureList.All();
                if (ForceTestData)
                    FixtureList.Test();
            }
            else
            {
                using (Context.Development)
                {
                    FixtureList.All();
                    FixtureList.Development();
                }

                using (Context.Test)
                {
                    FixtureList.All();
                    FixtureList.Test();
                }
            }
        }



        #endregion
    }
}
