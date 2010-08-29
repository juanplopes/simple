using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;

namespace Example.Project.Tools.Templates.Scaffold
{
    public class ScaffoldRemover : ICommand
    {
        public IList<string> ClassNames { get; set; }

        #region ICommand Members

        public void Execute()
        {
            using (var model = Options.Do.Model.Project)
            using (var server = Options.Do.Server.Project)
            {
                var list = ScaffoldGenerator.MakeTemplateList(model, server);
                foreach(var className in ClassNames)
                    foreach (var template in list)
                        template.Delete(className);
            }
        }

        #endregion
    }
}
