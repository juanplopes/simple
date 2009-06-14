using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.ConfigSource;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace Simple.Tests.SimpleLib
{
    [TestFixture]
    public class XmlConfigSourceFixture
    {
        public static void TestCreatedSimpleSample(BasicTypesSampleWithoutAttr config)
        {
            Assert.AreEqual("whatever", config.AString);
            Assert.AreEqual(42, config.AnIntegral);
            Assert.AreEqual(42.42, config.AFloat, 0.001);
        }

        public static string SAMPLE_XML =
                @"<BasicTypesSampleWithoutAttr>
                    <AString>whatever</AString>
                    <AnIntegral>42</AnIntegral>
                    <AFloat>42.42</AFloat>
                  </BasicTypesSampleWithoutAttr>";

        [Test]
        public void BasicTypesSampleFromStringTest()
        {
            IConfigSource<BasicTypesSampleWithoutAttr, string> src =
                new XmlConfigSource<BasicTypesSampleWithoutAttr>();
            TestCreatedSimpleSample(src.Load(SAMPLE_XML));
        }

        [Test]
        public void MoreInformationThanCanHandle()
        {
            string mySample = @"<BasicTypesSampleWithoutAttr>
                    <AString>whatever</AString>
                    <NotKnown>asd</NotKnown>
                  </BasicTypesSampleWithoutAttr>"; ;

            IConfigSource<BasicTypesSampleWithoutAttr, string> src =
                new XmlConfigSource<BasicTypesSampleWithoutAttr>();
            Assert.AreEqual("whatever", src.Load(mySample).AString);
        }


        [Test]
        public void BasicTypesSampleFromStreamTest()
        {
            using (MemoryStream mem = new MemoryStream(Encoding.UTF8.GetBytes(SAMPLE_XML)))
            {
                IConfigSource<BasicTypesSampleWithoutAttr, Stream> src =
                    new XmlConfigSource<BasicTypesSampleWithoutAttr>();

                TestCreatedSimpleSample(src.Load(mem));
            }
        }

        [Test]
        public void BasicTypesSampleFromNodeTest()
        {
            using (MemoryStream mem = new MemoryStream(Encoding.UTF8.GetBytes(SAMPLE_XML)))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(SAMPLE_XML);

                IConfigSource<BasicTypesSampleWithoutAttr, XmlDocument> src =
                    new XmlConfigSource<BasicTypesSampleWithoutAttr>();
                TestCreatedSimpleSample(src.Load(doc));
            }
        }

        [Test]
        public void MissingAStringTest()
        {
            string brokenXml =
                @"<BasicTypesSampleWithoutAttr>
                    <AnIntegral>42</AnIntegral>
                    <AFloat>42.42</AFloat>
                  </BasicTypesSampleWithoutAttr>";
            var src = new XmlConfigSource<BasicTypesSampleWithoutAttr>();
            var cfg = src.Load(brokenXml);
            Assert.IsNull(cfg.AString);
        }

        [Test]
        public void MissingAnIntegralTest()
        {
            string brokenXml =
                @"<BasicTypesSampleWithoutAttr>
                    <AFloat>42.42</AFloat>
                  </BasicTypesSampleWithoutAttr>";
            var src = new XmlConfigSource<BasicTypesSampleWithoutAttr>();
            var cfg = src.Load(brokenXml);
        }

        [Test]
        public void ComplexTypesTest()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(SAMPLE_XML);

            string complexXml =
                @"<ComplexTypesSample><Basic>%z</Basic><Basics>%s%s%s</Basics></ComplexTypesSample>"
                    .Replace("%s", doc.DocumentElement.OuterXml)
                    .Replace("%z", doc.DocumentElement.InnerXml);

            var src = new XmlConfigSource<ComplexTypesSample>();
            var cfg = src.Load(complexXml);

            TestCreatedSimpleSample(cfg.Basic);
            Assert.AreEqual(3, cfg.Basics.Length);
            foreach (var simple in cfg.Basics)
            {
                TestCreatedSimpleSample(simple);
            }
        }

        [Test]
        public void NotKnownTypesTest()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(SAMPLE_XML);

            string complexXml =
                @"<NotKnownTypeSample><Basic>%s</Basic><Basics>%s%s%s</Basics></NotKnownTypeSample>"
                    .Replace("%s", doc.DocumentElement.OuterXml)
                    .Replace("%z", doc.DocumentElement.InnerXml);

            var src = new XmlConfigSource<NotKnownTypeSample>();
            var cfg = src.Load(complexXml);

            var src2 = new XmlConfigSource<BasicTypesSampleWithoutAttr>();

            TestCreatedSimpleSample(src2.Load(cfg.Basic));
            Assert.AreEqual(3, cfg.Basics.Length);
            foreach (var simple in cfg.Basics)
            {
                TestCreatedSimpleSample(src2.Load(cfg.Basic));
            }
        }

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

    #endregion
}
