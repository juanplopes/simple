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
//using Sample.Project.Tools.Data.Development;

namespace Sample.Project.Tools.Data
{
    public class InsertDataCommand : ICommand
    {
        public bool ForceTestData { get; set; }

        #region ICommand Members

        public void Execute()
        {
            var samples = DataManager.FromAssembly(GetType().Assembly);

            samples.ExecuteAllThatMatches(null);
            if (ForceTestData)
                samples.ExecuteAllThatMatches(Configurator.Test);

            if (!Configurator.IsProduction)
            {
                using (Context.Development)
                    samples.ExecuteAllThatMatches(Configurator.Development);

                using (Context.Test)
                    samples.ExecuteAllThatMatches(Configurator.Test);
            }

        }





        #endregion
    }
}
