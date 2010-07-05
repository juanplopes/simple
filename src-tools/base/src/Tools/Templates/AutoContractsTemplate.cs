using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using System.Reflection;
using Simple;
using Simple.Services;

namespace Sample.Project.Tools.Templates
{
    public class AutoContractsTemplate : ICommand
    {
        public string FilePath()
        {
            return "Services/_AutoContracts.cs";
        }

        #region ICommand Members

        public void Execute()
        {
            var asm = Assembly.Load(Default.ServerAssembly);
            var types = asm.GetTypes().Where(x => x.CanAssign(typeof(IService))).ToList();

            var template = this.ToTemplate();
            template["services"] = types;

            Console.WriteLine(template);
        }

        #endregion
    }
}
