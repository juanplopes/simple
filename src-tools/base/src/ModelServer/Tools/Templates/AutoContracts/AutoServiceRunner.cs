using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Reflection;
using Simple.Services;
using Simple;
using Simple.NVelocity;

namespace Example.Project.Tools.Templates.AutoContracts
{
    public class AutoServiceRunner
    {
        public static List<ClassSignature> GetTypes()
        {
            var asm = typeof(ServerStarter).Assembly;
            var types = asm.GetTypes().Where(x => x.CanAssign(typeof(IService)))
                .Select(x => new ClassSignature(x).InitializeNamespaces()).ToList();
            return types;
        }

        public static string FilePath(string service)
        {
            return "Services/I{0}.cs".AsFormat(service);
        }

        public void Run()
        {
            var types = GetTypes();

            using (var project = Options.Do.Model.Project)
            {
                foreach (var type in types)
                {
                    var template = new SimpleTemplate(Templates.AutoService);
                    template["interface"] = "I" + type.Type.GetRealClassName();
                    template["service"] = type;
                    template["opt"] = Options.Do;

                    project.AddNewCompile(FilePath(type.Type.GetRealClassName()), template.ToString());
                }
            }
        }
    }
}
