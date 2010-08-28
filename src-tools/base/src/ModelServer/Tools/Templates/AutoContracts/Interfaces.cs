using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator.Misc;
using System.IO;
using Simple.Reflection;

namespace Example.Project.Tools.Templates.AutoContracts
{
    public class Interfaces
    {
        public static void HideInterfaces()
        {
            var types = AutoServiceRunner.GetTypes();
            var files = Directory.GetFiles(Options.Do.ServerDirectory, "*.cs", SearchOption.AllDirectories);

            ReplaceHide(types, files);
        }


        public static int ShowInterfaces()
        {
            var files = Directory.GetFiles(Options.Do.ServerDirectory, "*.cs", SearchOption.AllDirectories);

            return ReplaceShow(files);
        }

         private static void ReplaceHide(IEnumerable<ClassSignature> types, IEnumerable<string> files)
        {
            var replacer = new CSharpInterfaceReplacer();
            foreach (var file in files)
            {
                var content = File.ReadAllText(file);
                var oldContent = content;
                foreach (var type in types)
                {
                    content = replacer.ReplaceHide(content, "I" + type.Type.Name, "Simple.Services.IService");
                }
                if (content != oldContent)
                    File.WriteAllText(file, content);
            }
        }

        private static int ReplaceShow(string[] files)
        {
            var replacer = new CSharpInterfaceReplacer();
            int count = 0;
            foreach (var file in files)
            {
                var content = File.ReadAllText(file);
                var newContent = content;
                content = replacer.ReplaceShow(content, "Simple.Services.IService");
                if (newContent != content)
                {
                    File.WriteAllText(file, content);
                    count++;
                }
            }
            return count;
        }
    }
}
