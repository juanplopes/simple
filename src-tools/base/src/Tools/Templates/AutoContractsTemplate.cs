using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using System.Reflection;
using Simple;
using Simple.Services;
using Simple.Reflection;

namespace Sample.Project.Tools.Templates
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
            var asm = Assembly.Load(Default.ServerAssembly);
            var types = asm.GetTypes().Where(x => x.CanAssign(typeof(IService)))
                .Select(x => new ClassSignature(x).InitializeNamespaces()).ToList();

            foreach (var type in types)
            {
                var template = this.ToTemplate();
                template["interface"] = "I" + type.Type.GetRealClassName();
                template["service"] = type;
                template["namespace"] = Default.Namespace;

                using (var project = Default.ContractsProject.Writer())
                    project.AddNewCompile(FilePath(type.Type.GetRealClassName()), template.ToString());
            }
        }

        #endregion
    }
}
