using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Simple.ConfigSource
{
    public interface IXPathConfigSource<T, A> : IConfigSource<T, XPathParameter<A>>
    { }

    public class XPathParameter<T>
    {
        public const string Root = ".";

        public T Parameter { get; set; }
        public string XPath { get; set; }

        public XPathParameter(T param, string xPath)
        {
            Parameter = param;
            XPath = xPath ?? Root;
        }

        public XPathParameter(T param) : this(param, Root) { }


        public XPathParameter<Q> ToOther<Q>(Q param)
        {
            return new XPathParameter<Q>(param, XPath);
        }

        public static implicit operator XPathParameter<T>(T value)
        {
            return new XPathParameter<T>(value);
        }
    }
}
