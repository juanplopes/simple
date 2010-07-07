using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Simple.IO
{
    public class RootFinder
    {
        public static bool ChangeToPathOf(string filename, string content)
        {
            return ChangeToPathOf(null, filename, content);
        }
        public static bool ChangeToPathOf(DirectoryInfo cwd, string filename, string content)
        {
            var fileinfo = Find(cwd, filename, content);
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

        public static DirectoryInfo FindDirectoryOf(string filename, string content)
        {
            return FindDirectoryOf(null, filename, content);
        }

        public static DirectoryInfo FindDirectoryOf(DirectoryInfo cwd, string filename, string content)
        {
            cwd = cwd ?? new DirectoryInfo(Environment.CurrentDirectory);
            while (cwd.Parent != null)
            {
                var file = Path.Combine(cwd.FullName, filename);
                if (File.Exists(file) && File.ReadAllText(file).StartsWith(content, StringComparison.InvariantCultureIgnoreCase))
                    return cwd;

                cwd = cwd.Parent;
            }
            return cwd;
        }

        public static FileInfo Find(string filename, string content)
        {
            return Find(null, filename, content);
        }

        public static FileInfo Find(DirectoryInfo cwd, string filename, string content)
        {
            cwd = FindDirectoryOf(cwd, filename, content);
            return new FileInfo(Path.Combine(cwd.FullName, filename));
        }
    }
}
