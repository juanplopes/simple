using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace BasicLibrary.Configuration
{
    public class PlainXmlConfigElement : ConfigElement
    {
        public XmlElement MainXmlElement
        {
            get
            {
                return this.XmlElements[0];
            }
        }
    }
}
