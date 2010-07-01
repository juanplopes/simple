using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Simple.Generator
{
    public class RootFinder
    {
        public static bool ChangeToPathOf(string filename, string content)
        {
            var fileinfo = FindFor(new DirectoryInfo(Environment.CurrentDirectory), filename, content);
            if (fileinfo.Exists)
            {
                Environment.CurrentDirectory = fileinfo.Directory.FullName;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static FileInfo FindFor(DirectoryInfo cwd, string filename, string content)
        {
            while (cwd.Parent != null)
            {
                var file = Path.Combine(cwd.FullName, filename);
                if (File.Exists(file) && File.ReadAllText(file).StartsWith(content, StringComparison.InvariantCultureIgnoreCase))
                    return new FileInfo(file);

                cwd = cwd.Parent;
            }

            return new FileInfo(Path.Combine(cwd.FullName, filename));
        }
    }
}
