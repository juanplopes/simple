using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.ConfigSource;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Simple.Logging;

namespace Simple.Tests.ConfigSource
{
    [TestClass]
    public class FactoryFixture
    {
        [TestInitialize]
        public void Setup()
        {
            File.WriteAllText(XmlFileConfigSourceFixture.TEST_FILE_NAME, XmlConfigSourceFixture.SAMPLE_XML);
        }

        [TestCleanup]
        public void Teardown()
        {
            if (File.Exists(XmlFileConfigSourceFixture.TEST_FILE_NAME))
                File.Delete(XmlFileConfigSourceFixture.TEST_FILE_NAME);
        }

        [TestMethod]
        public void SimpleFactoringTest()
        {
            IConfigSource<BasicTypesSampleWithoutAttr> src =
                new XmlConfigSource<BasicTypesSampleWithoutAttr>().Load(
                XmlConfigSourceFixture.SAMPLE_XML);

            BasicFactory b = new BasicFactory();
            (b as IFactory<BasicTypesSampleWithoutAttr>).Init(src);

            Assert.AreEqual(42, b.BuildInt());
            Assert.AreEqual("whatever", b.BuildString());
        }

        [TestMethod]
        public void SourcesFactoredTest()
        {
            IConfigSource<BasicTypesSampleWithoutAttr> src =
                new XmlConfigSource<BasicTypesSampleWithoutAttr>().Load(
                XmlConfigSourceFixture.SAMPLE_XML);

            SourceManager.Do.Remove<BasicTypesSampleWithoutAttr>();
            SourceManager.Do.Register(src);

            var b = new BasicFactory();
            SourceManager.Do.AttachFactory(b);

            Assert.AreEqual(42, b.BuildInt());
            Assert.AreEqual("whatever", b.BuildString());
        }

        [TestMethod]
        public void RedoSourcesFactoredTest()
        {
            IConfigSource<BasicTypesSampleWithoutAttr> src =
                new XmlConfigSource<BasicTypesSampleWithoutAttr>().Load(
                XmlConfigSourceFixture.SAMPLE_XML);

            SourceManager.Do.Remove<BasicTypesSampleWithoutAttr>();

            var b = new BasicFactory();

            SourceManager.Do.AttachFactory(b);
            Assert.AreEqual(default(string), b.BuildString());
            Assert.AreEqual(default(int), b.BuildInt());

            SourceManager.Do.Register(src);
            Assert.AreEqual("whatever", b.BuildString());
            Assert.AreEqual(42, b.BuildInt());

            SourceManager.Do.Remove<BasicTypesSampleWithoutAttr>();
            Assert.AreEqual(default(string), b.BuildString());
            Assert.AreEqual(default(int), b.BuildInt());
        }

        [TestMethod]
        public void RedoSourcesFactoredTestWithKey()
        {
            IConfigSource<BasicTypesSampleWithoutAttr> src =
                new XmlConfigSource<BasicTypesSampleWithoutAttr>().Load(
                XmlConfigSourceFixture.SAMPLE_XML);

            SourceManager.Do.Clear<BasicTypesSampleWithoutAttr>();

            var b = new BasicFactory();

            SourceManager.Do.AttachFactory(2, b);
            Assert.AreEqual(default(string), b.BuildString());
            Assert.AreEqual(default(int), b.BuildInt());

            SourceManager.Do.Register(2, src);
            Assert.AreEqual("whatever", b.BuildString());
            Assert.AreEqual(42, b.BuildInt());

            SourceManager.Do.Remove<BasicTypesSampleWithoutAttr>(2);
            Assert.AreEqual(default(string), b.BuildString());
            Assert.AreEqual(default(int), b.BuildInt());
        }


        [TestMethod]
        public void ExpiringFactoringTest()
        {
            using (var src = XmlConfig.LoadFile<BasicTypesSampleWithoutAttr>
                (XmlFileConfigSourceFixture.TEST_FILE_NAME))
            {
                BasicFactory b = new BasicFactory();
                (b as IFactory<BasicTypesSampleWithoutAttr>).Init(src);

                bool flag = false;

                src.Reloaded += x =>
                {
                    Assert.AreEqual(43, b.BuildInt());
                    Assert.AreEqual("whatever2", b.BuildString());
                    flag = true;
                };

                Assert.AreEqual(42, b.BuildInt());

                File.WriteAllText(XmlFileConfigSourceFixture.TEST_FILE_NAME, XmlFileConfigSourceFixture.SAMPLE_XML);

                long time = DateTime.Now.Ticks;
                while (!flag && new TimeSpan(DateTime.Now.Ticks - time).TotalSeconds < 3) ;

                Assert.IsTrue(flag);
            }
        }

        [TestMethod]
        public void WrappedConfigTest()
        {
            IWrappedConfigSource<BasicTypesSampleWithoutAttr> src = new WrappedConfigSource<BasicTypesSampleWithoutAttr>();
            src.Load(new XmlConfigSource<BasicTypesSampleWithoutAttr>().Load(
                XmlConfigSourceFixture.SAMPLE_XML));

            var b = src.Get();
            XmlConfigSourceFixture.TestCreatedSimpleSample(b);

            src.Load(NullConfigSource<BasicTypesSampleWithoutAttr>.Instance);
            b = src.Get();
            Assert.AreEqual(b, null);
        }

        [TestMethod]
        public void WrappedConfigFactoredTest()
        {
            IWrappedConfigSource<BasicTypesSampleWithoutAttr> src = new WrappedConfigSource<BasicTypesSampleWithoutAttr>();
            src.Load(new XmlConfigSource<BasicTypesSampleWithoutAttr>().Load(
                XmlConfigSourceFixture.SAMPLE_XML));

            SourceManager.Do.Clear<BasicTypesSampleWithoutAttr>();
            SourceManager.Do.Register(src);

            var f = new BasicFactory();
            SourceManager.Do.AttachFactory(f);
            Assert.AreEqual("whatever", f.BuildString());
            Assert.AreEqual(42, f.BuildInt());

            src.Load(NullConfigSource<BasicTypesSampleWithoutAttr>.Instance);
            Assert.AreEqual(default(string), f.BuildString());
            Assert.AreEqual(default(int), f.BuildInt());
        }

        [TestMethod]
        public void WrappedFactoryConfigTest()
        {
            IWrappedConfigSource<BasicTypesSampleWithoutAttr> src = new WrappedConfigSource<BasicTypesSampleWithoutAttr>();
            src.Load(new XmlConfigSource<BasicTypesSampleWithoutAttr>().Load(
                XmlConfigSourceFixture.SAMPLE_XML));

            FactoryManager<BasicFactory, BasicTypesSampleWithoutAttr> factories = 
                new FactoryManager<BasicFactory, BasicTypesSampleWithoutAttr>(() => new BasicFactory());

            var f = factories.SafeGet();
            Assert.AreEqual(default(string), f.BuildString());
            Assert.AreEqual(default(int), f.BuildInt());

            SourceManager.Do.Register(src);
            Assert.AreEqual("whatever", f.BuildString());
            Assert.AreEqual(42, f.BuildInt());
        }

        [TestMethod]
        public void WrappedKeyedFactoryConfigTest()
        {
            IWrappedConfigSource<BasicTypesSampleWithoutAttr> src = new WrappedConfigSource<BasicTypesSampleWithoutAttr>();
            src.Load(new XmlConfigSource<BasicTypesSampleWithoutAttr>().Load(
                XmlConfigSourceFixture.SAMPLE_XML));

            FactoryManager<BasicFactory, BasicTypesSampleWithoutAttr> factories =
                new FactoryManager<BasicFactory, BasicTypesSampleWithoutAttr>(() => new BasicFactory());

            object key = new object();

            var f = factories[key];
            Assert.AreEqual(default(string), f.BuildString());
            Assert.AreEqual(default(int), f.BuildInt());

            SourceManager.Do.Register(key, src);
            Assert.AreEqual("whatever", f.BuildString());
            Assert.AreEqual(42, f.BuildInt());
        }
    }
    #region Samples

    public class BasicFactory : Factory<BasicTypesSampleWithoutAttr>
    {
        string value1;
        int value2;

        protected override void OnConfig(BasicTypesSampleWithoutAttr config)
        {
            value1 = config.AString;
            value2 = config.AnIntegral;
        }

        protected override void OnClearConfig()
        {
            value1 = default(string);
            value2 = default(int);
        }

        public string BuildString()
        {
            return value1;
        }

        public int BuildInt()
        {
            return value2;
        }
    }

    #endregion
}
