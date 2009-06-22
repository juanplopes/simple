using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simple.ConfigSource;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace Simple.Tests.ConfigSource
{
    [TestClass]
    public class XmlConfigSourceFixture
    {
        public static void TestCreatedSimpleSample(BasicTypesSampleWithoutAttr config)
        {
            Assert.AreEqual("whatever", config.AString);
            Assert.AreEqual(42, config.AnIntegral);
            Assert.AreEqual(42.42, config.AFloat, 0.001);
        }

        public const string SAMPLE_XML =
                @"<BasicTypesSampleWithoutAttr>
                    <AString>whatever</AString>
                    <AnIntegral>42</AnIntegral>
                    <AFloat>42.42</AFloat>
                  </BasicTypesSampleWithoutAttr>";

        public const string XPATH_OUTER = "./BasicTypesSampleWithoutAttr";

        public const string XPATH_SAMPLE_XML = "<test>" + SAMPLE_XML + "</test>";


        public void BasicStringTestBase(string xml, string xpath)
        {
            IXPathConfigSource<BasicTypesSampleWithoutAttr, string> src =
                new XmlConfigSource<BasicTypesSampleWithoutAttr>();
            TestCreatedSimpleSample(src.Load(new XPathParameter<string>(xml, xpath)).Get());
        }

        public void BasicStreamTestBase(string xml, string xpath)
        {
            using (MemoryStream mem = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                IXPathConfigSource<BasicTypesSampleWithoutAttr, Stream> src =
                    new XmlConfigSource<BasicTypesSampleWithoutAttr>();

                TestCreatedSimpleSample(src.Load(new XPathParameter<Stream>(mem, xpath)).Get());
            }
        }

        public void BasicXmlElementTestBase(string xml, string xpath)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            IXPathConfigSource<BasicTypesSampleWithoutAttr, XmlElement> src =
                new XmlConfigSource<BasicTypesSampleWithoutAttr>();
            TestCreatedSimpleSample(src.Load(new XPathParameter<XmlElement>(
                doc.DocumentElement, xpath)).Get());
        }

        public void BasicXmlDocumentTestBase(string xml, string xpath)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            IXPathConfigSource<BasicTypesSampleWithoutAttr, XmlDocument> src =
                new XmlConfigSource<BasicTypesSampleWithoutAttr>();
            TestCreatedSimpleSample(src.Load(new XPathParameter<XmlDocument>(doc, xpath)).Get());

        }

        public void BasicTextReaderTestBase(string xml, string xpath)
        {
            StringReader reader = new StringReader(xml);

            IXPathConfigSource<BasicTypesSampleWithoutAttr, TextReader> src =
                new XmlConfigSource<BasicTypesSampleWithoutAttr>();
            TestCreatedSimpleSample(src.Load(new XPathParameter<TextReader>(reader, xpath)).Get());
        }

        public void BasicXmlReaderTestBase(string xml, string xpath)
        {
            StringReader reader = new StringReader(xml);
            XmlTextReader reader2 = new XmlTextReader(reader);

            IXPathConfigSource<BasicTypesSampleWithoutAttr, XmlReader> src =
                new XmlConfigSource<BasicTypesSampleWithoutAttr>();

            TestCreatedSimpleSample(src.Load(new XPathParameter<XmlReader>(reader2, xpath)).Get());

        }

        public void BasicXmlContentHolderTestBase(string xml, string xpath)
        {
            IXPathConfigSource<XmlContainerSample, string> src =
                new XmlConfigSource<XmlContainerSample>();
            XmlContainerSample samp = src.Load(new XPathParameter<string>(xml, xpath)).Get();

            IXPathConfigSource<BasicTypesSampleWithoutAttr, XmlElement> src2 =
                new XmlConfigSource<BasicTypesSampleWithoutAttr>();

            TestCreatedSimpleSample(src2.Load(samp.Element).Get());
        }

        #region Basic
        [TestMethod]
        public void BasicStringTest()
        {
            BasicStringTestBase(SAMPLE_XML, null);
        }

        [TestMethod]
        public void BasicStreamTest()
        {
            BasicStreamTestBase(SAMPLE_XML, null);
        }

        [TestMethod]
        public void BasicXmlElementTest()
        {
            BasicXmlElementTestBase(SAMPLE_XML, null);
        }

        [TestMethod]
        public void BasicXmlDocumentTest()
        {
            BasicXmlDocumentTestBase(SAMPLE_XML, null);
        }

        [TestMethod]
        public void BasicTextReaderTest()
        {
            BasicTextReaderTestBase(SAMPLE_XML, null);
        }

        [TestMethod]
        public void BasicXmlReaderTest()
        {
            BasicXmlReaderTestBase(SAMPLE_XML, null);
        }

        [TestMethod]
        public void BasicXmlContentHolderTest()
        {
            BasicXmlContentHolderTestBase(SAMPLE_XML, null);
        }

        #endregion


        #region XPath
        [TestMethod]
        public void XPathStringTest()
        {
            BasicStringTestBase(XPATH_SAMPLE_XML, XPATH_OUTER);
        }

        [TestMethod]
        public void XPathStreamTest()
        {
            BasicStreamTestBase(XPATH_SAMPLE_XML, XPATH_OUTER);
        }

        [TestMethod]
        public void XPathXmlElementTest()
        {
            BasicXmlElementTestBase(XPATH_SAMPLE_XML, XPATH_OUTER);
        }

        [TestMethod]
        public void XPathXmlDocumentTest()
        {
            BasicXmlDocumentTestBase(XPATH_SAMPLE_XML, XPATH_OUTER);
        }

        [TestMethod]
        public void XPathTextReaderTest()
        {
            BasicTextReaderTestBase(XPATH_SAMPLE_XML, XPATH_OUTER);
        }

        [TestMethod]
        public void XPathXmlReaderTest()
        {
            BasicXmlReaderTestBase(XPATH_SAMPLE_XML, XPATH_OUTER);
        }

        [TestMethod]
        public void XPathXmlContentHolderTest()
        {
            BasicXmlContentHolderTestBase(XPATH_SAMPLE_XML, XPATH_OUTER);
        }


        #endregion

        [TestMethod]
        public void MoreInformationThanCanHandle()
        {
            string mySample = @"<BasicTypesSampleWithoutAttr>
                    <AString>whatever</AString>
                    <NotKnown>asd</NotKnown>
                  </BasicTypesSampleWithoutAttr>"; ;

            IXPathConfigSource<BasicTypesSampleWithoutAttr, string> src =
                new XmlConfigSource<BasicTypesSampleWithoutAttr>();
            Assert.AreEqual("whatever", src.Load(mySample).Get().AString);
        }

        [TestMethod]
        public void MissingAStringTest()
        {
            string brokenXml =
                @"<BasicTypesSampleWithoutAttr>
                    <AnIntegral>42</AnIntegral>
                    <AFloat>42.42</AFloat>
                  </BasicTypesSampleWithoutAttr>";
            var src = new XmlConfigSource<BasicTypesSampleWithoutAttr>();
            var cfg = src.Load(brokenXml).Get();
            Assert.IsNull(cfg.AString);
        }

        [TestMethod]
        public void MissingAnIntegralTest()
        {
            string brokenXml =
                @"<BasicTypesSampleWithoutAttr>
                    <AFloat>42.42</AFloat>
                  </BasicTypesSampleWithoutAttr>";
            var src = new XmlConfigSource<BasicTypesSampleWithoutAttr>();
            var cfg = src.Load(brokenXml).Get();
        }

        [TestMethod]
        public void ComplexTypesTest()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(SAMPLE_XML);

            string complexXml =
                @"<ComplexTypesSample><Basic>%z</Basic><Basics>%s%s%s</Basics></ComplexTypesSample>"
                    .Replace("%s", doc.DocumentElement.OuterXml)
                    .Replace("%z", doc.DocumentElement.InnerXml);

            var src = new XmlConfigSource<ComplexTypesSample>();
            var cfg = src.Load(complexXml).Get();

            TestCreatedSimpleSample(cfg.Basic);
            Assert.AreEqual(3, cfg.Basics.Length);
            foreach (var simple in cfg.Basics)
            {
                TestCreatedSimpleSample(simple);
            }
        }

        [TestMethod]
        public void NotKnownTypesTest()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(SAMPLE_XML);

            string complexXml =
                @"<NotKnownTypeSample><Basic>%s</Basic><Basics>%s%s%s</Basics></NotKnownTypeSample>"
                    .Replace("%s", doc.DocumentElement.OuterXml)
                    .Replace("%z", doc.DocumentElement.InnerXml);

            var src = new XmlConfigSource<NotKnownTypeSample>();
            var cfg = src.Load(complexXml).Get();

            var src2 = new XmlConfigSource<BasicTypesSampleWithoutAttr>();

            TestCreatedSimpleSample(src2.Load(cfg.Basic).Get());
            Assert.AreEqual(3, cfg.Basics.Length);
            foreach (var simple in cfg.Basics)
            {
                TestCreatedSimpleSample(src2.Load(cfg.Basic).Get());
            }
        }

        //[TestMethod]
        //public void WithRepositoryTest()
        //{
        //    IConfigSource<BasicTypesSampleWithoutAttr, string> src =
        //        new XmlConfigSource<BasicTypesSampleWithoutAttr>();

        //    ConfigRepository conf = new ConfigRepository();
        //    conf.SetSource(src.Load(SAMPLE_XML));

        //    TestCreatedSimpleSample(conf.Get<BasicTypesSampleWithoutAttr>());
        //}B

        //[TestMethod]
        //public void WithConfigSourcesTest()
        //{
        //    var src = new XmlConfigSource<BasicTypesSampleWithoutAttr>();
        //    ConfigSources.SetDefaultSource(src.Load(SAMPLE_XML));
        //    TestCreatedSimpleSample(ConfigSources.GetDefault<BasicTypesSampleWithoutAttr>());
        //}



        //[Test, ExpectedException(typeof(KeyNotFoundException))]
        //public void WithRepositoryFailure()
        //{
        //    IConfigSource<BasicTypesSampleWithoutAttr, string> src =
        //        new XmlConfigSource<BasicTypesSampleWithoutAttr>();

        //    ConfigRepository conf = new ConfigRepository();
        //    //conf.SetSource(src.Load(SAMPLE_XML));

        //    TestCreatedSimpleSample(conf.Get<BasicTypesSampleWithoutAttr>());
        //}
    }

    #region Samples

    public class BasicTypesSampleWithoutAttr
    {
        public string AString { get; set; }
        public int AnIntegral { get; set; }
        public float AFloat { get; set; }
    }

    public class ComplexTypesSample
    {
        public BasicTypesSampleWithoutAttr Basic { get; set; }
        public BasicTypesSampleWithoutAttr[] Basics { get; set; }
    }

    public class NotKnownTypeSample
    {
        public XmlElement Basic { get; set; }
        [XmlArrayItem("BasicTypesSampleWithoutAttr")]
        public XmlElement[] Basics { get; set; }
    }

    public class XmlContainerSample : IXmlContentHolder
    {
        public XmlElement Element { get; set; }
    }

    #endregion
}
