using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Simple.IO
{
    public class FileLocator : List<string>
    {
        public IEnumerable<string> ExistsWhere(string file)
        {
            string temp;
            foreach (var path in this)
                if (File.Exists(temp = Path.Combine(path, file)))
                    yield return temp;
        }

        public string Find(string file, bool shouldThrow)
        {
            string path = ExistsWhere(file).FirstOrDefault();
            if (path == null && shouldThrow)
                throw new FileNotFoundException(string.Format("Search locations: {0}", string.Join(", ", this.ToArray())));

            return path;
        }

        public string Find(string file)
        {
            return Find(file, true);
        }
    }
}
