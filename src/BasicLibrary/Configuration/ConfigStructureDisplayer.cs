using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Reflection;
using BasicLibrary.Common;
using System.Collections;

namespace BasicLibrary.Configuration
{
    public class ConfigStructureDisplayer
    {
        public Type ConfigType { get; set; }

        public ConfigStructureDisplayer(Type configType)
        {
            ConfigType = configType;
        }

        public XmlDocument CreateSampleStructure()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("root");
            doc.AppendChild(root);
            CreateSampleStructure(doc, root);

            return doc;
        }

        public string FormatSampleStructure()
        {
            StringBuilder builder = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true; settings.IndentChars = new string(' ', 2);
            XmlWriter writer = XmlWriter.Create(builder, settings);
            CreateSampleStructure().WriteTo(writer);
            writer.Flush();

            return builder.ToString();
        }

        public void CreateSampleStructure(XmlDocument doc, XmlElement root)
        {
            foreach (PropertyInfo property in ConfigType.GetProperties())
            {
                ConfigElementAttribute attr = ListExtensor.GetFirst<ConfigElementAttribute>(
                    property.GetCustomAttributes(typeof(ConfigElementAttribute), true));
                if (attr != null)
                {
                    Type propType = property.PropertyType;
                    if (typeof(IList).IsAssignableFrom(propType) && propType.GetGenericArguments().Length == 1)
                    {
                        root.AppendChild(doc.CreateComment("The element bellow is a list"));
                        propType = propType.GetGenericArguments()[0];
                    }
                    else if (typeof(IDictionary).IsAssignableFrom(propType) && propType.GetGenericArguments().Length == 2)
                    {
                        root.AppendChild(doc.CreateComment("The element bellow is a dictionary"));
                        propType = propType.GetGenericArguments()[1];
                    }




                    XmlElement element = doc.CreateElement(attr.Name);
                    root.AppendChild(element);

                    if (typeof(IConfigElement).IsAssignableFrom(propType))
                    {
                        ConfigStructureDisplayer disp = new ConfigStructureDisplayer(propType);
                        disp.CreateSampleStructure(doc, element);
                    }
                    else
                    {
                        element.InnerText = propType.FullName;
                    }
                }
            }
        }
    }
}
