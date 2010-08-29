using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Reflection;
using Simple.Services;
using Simple;
using Simple.NVelocity;
using Simple.Entities;

namespace Example.Project.Tools.Templates.AutoContracts
{
    public class AutoDomainRunner
    {
        public static List<ClassSignature> GetTypes()
        {
            var asm = typeof(ServerStarter).Assembly;
            var types = asm.GetTypes().Where(x => x.CanAssign(typeof(IService)) && 
                    x.GetTypeArgumentsFor(typeof(IEntityService<>)).Length != 0 )
                .Select(x => new ClassSignature(x).InitializeNamespaces()).ToList();
            return types;
        }

        public static string FilePath(string entity)
        {
            return "Domain/Generated/{0}.contracts.cs".AsFormat(entity);
        }

        public void Run()
        {
            var types = GetTypes();

            using (var project = Options.Do.Model.Project)
            {
                foreach (var type in types)
                {
                    var entity = type.Type.GetTypeArgumentsFor(typeof(IEntityService<>)).FirstOrDefault();
                    var template = new SimpleTemplate(Templates.AutoDomain);
                    template["entity"] = entity;
                    template["service"] = type;
                    template["opt"] = Options.Do;

                    project.AddNewCompile(FilePath(entity.Name), template.ToString());
                }
            }
        }
    }
}
