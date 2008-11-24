using System;
using System.Xml;
using System.Collections.Generic;
namespace BasicLibrary.Configuration
{
    public interface IConfigElement
    {
        string DefaultXmlString { get; }
        void InvokeExpire();
        void LoadFromElement(IConfigElement element);
        void LoadFromElement(XmlElement element);
        void Lock();
        event EventHandler OnExpire;
        IList<XmlElement> XmlElements { get; set; }
    }
}
