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
        public bool ForceTestData { get; set; }

        #region ICommand Members

        public void Execute()
        {
            if (Configurator.IsProduction)
            {
                All();
                if (ForceTestData)
                    Test();
            }
            else
            {
                using (Context.Development)
                    All().Development();

                using (Context.Test)
                    All().Test();
            }
        }

        public InsertDataCommand All()
        {
            return this;
        }


        public InsertDataCommand Test()
        {
            return this;
        }

        public InsertDataCommand Development()
        {
            return this;
        }




        #endregion
    }
}
