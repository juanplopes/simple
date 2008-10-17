using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using BasicLibrary.Logging;

namespace BasicLibrary.Common
{
    public class FileContentCache
    {
        protected static Dictionary<string, string> Files { get; set; }
        protected static DateTime LastRefresh { get; set; }
        protected const int RefreshTime = 10;

        static FileContentCache()
        {
            Files = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            LastRefresh = DateTime.Now;
        }

        public static XmlElement GetAsXmlElement(string fileName)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(Get(fileName));
            return document.DocumentElement;
        }

        private static void CheckRefresh()
        {
            if (DateTime.Now.Subtract(LastRefresh).TotalMinutes > RefreshTime)
            {
                Files.Clear();
            }
        }

        public static string Get(string fileName)
        {
            lock (Files)
            {
                CheckRefresh();
                fileName = GetBasedFile(fileName);

                if (!Files.ContainsKey(fileName))
                {
                    return Files[fileName] = File.ReadAllText(fileName);
                }
                else
                {
                    return Files[fileName];
                }
            }
        }

        public static string GetBasedFile(string file)
        {
            string oldFile = file;
            if (!Path.IsPathRooted(file))
            {
                if (AppDomain.CurrentDomain.RelativeSearchPath != null)
                    file = Path.Combine(AppDomain.CurrentDomain.RelativeSearchPath, oldFile);
                if (AppDomain.CurrentDomain.RelativeSearchPath == null || !File.Exists(file))
                    file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, oldFile);
            }
            return file;
        }
    }
}
