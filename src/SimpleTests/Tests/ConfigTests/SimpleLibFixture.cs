using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Config;
using System.IO;
using System.Xml;

namespace Simple.Tests.ConfigTests
{
    [TestFixture]
    public class SimpleLibFixture
    {
        [Test]
        public void TestCorrectFileLoad()
        {
            var list = SimpleLibConfig.Get().XmlElements;
            Assert.AreEqual(2, list.Count);


            XmlDocument doc = new XmlDocument();
            doc.Load("Simple.config");

            Assert.AreEqual(list[1].OuterXml, doc.DocumentElement.OuterXml);
        }
    }
}
