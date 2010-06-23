using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Simple;
using System.IO;
using log4net;
using System.Reflection;

namespace Simple.Generator
{
    public class ProjectWriter
    {
        XmlDocument _doc = null;
        XmlNamespaceManager _names = null;
        string _namespace = null;
        private const string DefaultNamespace = "http://schemas.microsoft.com/developer/msbuild/2003";
        ILog log = Simply.Do.Log(MethodInfo.GetCurrentMethod());


        public ProjectWriter(string xml) : this(xml, DefaultNamespace) { }
        public ProjectWriter(string xml, string ns)
        {
            _doc = new XmlDocument();
            _namespace = ns;
            _names = new XmlNamespaceManager(_doc.NameTable);
            _names.AddNamespace("p", _namespace);
            _doc.LoadXml(xml);
        }
        
        public const string Compile = "Compile";
        public ProjectWriter AddCompile(string file)
        {
            return AddFile(file, Compile);
        }

        public const string EmbeddedResource = "EmbeddedResource";
        public ProjectWriter AddEmbeddedResource(string file)
        {
            return AddFile(file, EmbeddedResource);
        }

        public ProjectWriter AddFile(string file, string type)
        {
            log.DebugFormat("Adding file '{0}' to project as '{1}'...", file, type);

            var node = GetFileNode(file);
            if (node != null) return this;

            file = file.Replace(@"/", @"\");
            XmlNode nodeItemGroup = _doc.SelectSingleNode("//p:ItemGroup[p:{0}]".AsFormat(type), _names);
            XmlElement newChild = _doc.CreateElement(type, _namespace);
            newChild.SetAttribute("Include", file);
            nodeItemGroup.AppendChild(newChild);
            return this;
        }

        public ProjectWriter RemoveFile(string file)
        {
            log.DebugFormat("Removing file '{0}' from project...", file);

            XmlNode nodeItemGroup = GetFileNode(file);
            nodeItemGroup.ParentNode.RemoveChild(nodeItemGroup);
            return this;
        }

        private XmlNode GetFileNode(string file)
        {
            file = file.Replace(@"/", @"\");
            return _doc.SelectSingleNode("//p:ItemGroup/p:*[@Include=\"{0}\"]".AsFormat(file), _names);
        }


        public string GetXml()
        {
            using (var memory = new MemoryStream())
            {
                WriteXmlTo(memory);
                memory.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(memory))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public ProjectWriter WriteXmlTo(Stream stream)
        {
            _doc.Save(stream);
            return this;
        }
    }
}
