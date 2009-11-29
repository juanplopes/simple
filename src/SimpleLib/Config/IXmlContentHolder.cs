using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Simple.Config
{
    public interface IXmlContentHolder
    {
        XmlElement Element { get; set; }
    }
}
