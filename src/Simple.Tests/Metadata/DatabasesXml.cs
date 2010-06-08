using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Simple.Tests.Metadata
{
    [XmlRoot("Databases")]
    public class DatabasesXml
    {
        public class Entry
        {
            [XmlAttribute]
            public string ConnectionString { get; set; }
            [XmlAttribute]
            public string Provider { get; set; }

            public override string ToString()
            {
                return Provider;
            }
        }

        [XmlElement("Entry")]
        public List<Entry> Entries { get; set; }
    }
}
