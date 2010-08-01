using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using System.Reflection;
using Simple;
using Simple.Services;
using Simple.Reflection;
using Simple.NVelocity;

namespace Example.Project.Tools.Templates
{
    public class AutoContractsTemplate : ICommand
    {
        public string FilePath(string service)
        {
            return "Services/I{0}.cs".AsFormat(service);
        }

        #region ICommand Members

        public void Execute()
        {
            var asm = Assembly.Load(Options.Do.ServerAssembly);
            var types = asm.GetTypes().Where(x => x.CanAssign(typeof(IService)))
                .Select(x => new ClassSignature(x).InitializeNamespaces()).ToList();

            using (var project = Options.Do.ContractsProject)
            {
                foreach (var type in types)
                {
                    var template = new SimpleTemplate(Templates.AutoContracts);
                    template["interface"] = "I" + type.Type.GetRealClassName();
                    template["service"] = type;
                    template["opt"] = Options.Do;

                    project.AddNewCompile(FilePath(type.Type.GetRealClassName()), template.ToString());
                }
            }
        }

        #endregion
    }
}
