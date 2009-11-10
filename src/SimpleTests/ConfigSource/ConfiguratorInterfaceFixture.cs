using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.ConfigSource;
using System.IO;

namespace Simple.Tests.ConfigSource
{
    [TestFixture]
    public class ConfiguratorInterfaceFixture
    {
        [Test]
        public void LoadFromXmlTest()
        {
            IConfigSource<BasicTypesSampleWithoutAttr> source = null;
            new ConfiguratorInterface<BasicTypesSampleWithoutAttr, int>(
                x => { source = x; return 1; }).FromXmlString(XmlConfigSourceFixture.SAMPLE_XML);

            Assert.IsNotNull(source);
            var value = source.Get();

            XmlConfigSourceFixture.TestCreatedSimpleSample(value);
        }

        [Test]
        public void LoadFromStreamTest()
        {
            IConfigSource<BasicTypesSampleWithoutAttr> source = null;
            new ConfiguratorInterface<BasicTypesSampleWithoutAttr, int>(
                x => { source = x; return 1; }).FromStream(
                new MemoryStream(Encoding.UTF8.GetBytes(XmlConfigSourceFixture.SAMPLE_XML)));

            Assert.IsNotNull(source);
            var value = source.Get();

            XmlConfigSourceFixture.TestCreatedSimpleSample(value);
        }

        [Test]
        public void LoadFromInstanceTest()
        {
            IConfigSource<BasicTypesSampleWithoutAttr> source = null;
            new ConfiguratorInterface<BasicTypesSampleWithoutAttr, int>(
                x => { source = x; return 1; }).FromInstance(new BasicTypesSampleWithoutAttr()
                {
                    AFloat = 42.42f,
                    AnIntegral = 42,
                    AString = "whatever"
                });

            Assert.IsNotNull(source);
            var value = source.Get();

            XmlConfigSourceFixture.TestCreatedSimpleSample(value);
        }
    }
}
