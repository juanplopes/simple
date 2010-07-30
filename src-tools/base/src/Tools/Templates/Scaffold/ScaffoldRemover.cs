using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;

namespace Sample.Project.Tools.Templates.Scaffold
{
    public class ScaffoldRemover : ICommand
    {
        public IList<string> ClassNames { get; set; }

        #region ICommand Members

        public void Execute()
        {
            using (var contracts = Options.Do.ContractsProject)
            using (var server = Options.Do.ServerProject)
            {
                var list = ScaffoldGenerator.MakeTemplateList(contracts, server);
                foreach(var className in ClassNames)
                    foreach (var template in list)
                        template.Delete(className);
            }
        }

        #endregion
    }
}
