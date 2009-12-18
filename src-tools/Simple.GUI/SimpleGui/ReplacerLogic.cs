using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Simple.GUI
{
    public class ReplacerLogic
    {
        public static void DefaultExecute(string path, string find, string replace)
        {
            Execute(path, find, replace,
                "cmd", "csproj", "sln", "xml", "txt", "config", 
                "cs", "aspx", "resx", "tt", "ttinclude", "asax", "sql");
        }

        public static void Execute(string path, string find, string replace, params string[] extensions)
        {
            foreach (string s in extensions)
            {
                var ext = "*." + s;
                var files = Directory.GetFiles(path, ext, SearchOption.AllDirectories);

                foreach (var file in files)
                {
                    string content = File.ReadAllText(file);
                    if (content.Contains(find))
                    {
                        content = content.Replace(find, replace);
                        File.WriteAllText(file, content);
                    }
                }
            }
        }
    }
}
