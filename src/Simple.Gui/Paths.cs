using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Simple.Gui
{
    public class Paths : IEnumerable<string>
    {
        protected List<string> PathDirectories { get;  set ; }

        public Paths(string path)
        {
            this.PathDirectories = ExtractPathFolders(path).ToList();
        }

        private static IEnumerable<string> ExtractPathFolders(string path)
        {
            return (path ?? "").Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public bool Contains(string path)
        {
            path = Normalize(path);

            return PathDirectories.Select(x => Normalize(x)).Contains(path);
        }

        public string Add(string path)
        {
            path = Normalize(path);
            PathDirectories.Add(path);
            return path;
        }

        public static string Normalize(string path)
        {
            if (path == null) return "";

            path = path.Replace("/", @"\");
            if (!path.EndsWith(@"\")) path += @"\";

            return path.ToLower();
        }


        public override string ToString()
        {
            return string.Join(";", PathDirectories.ToArray());
        }


        #region IEnumerable<string> Members

        public IEnumerator<string> GetEnumerator()
        {
            return PathDirectories.GetEnumerator();
        }


        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}

