using System;
using System.IO;
using NUnit.Framework;
using SharpTestsEx;
using Simple.Config;

namespace Simple.Tests.Config
{
    [TestFixture]
    public class FactoryFixture
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            File.WriteAllText(XmlFileConfigSourceFixture.TEST_FILE_NAME, XmlConfigSourceFixture.SAMPLE_XML);
        }

        [TestFixtureTearDown]
        public void Teardown()
        {
            if (File.Exists(XmlFileConfigSourceFixture.TEST_FILE_NAME))
                File.Delete(XmlFileConfigSourceFixture.TEST_FILE_NAME);
        }

        [Test]
        public void SimpleFactoringTest()
        {
            IConfigSource<BasicTypesSampleWithoutAttr> src =
                new XmlConfigSource<BasicTypesSampleWithoutAttr>().Load(
                XmlConfigSourceFixture.SAMPLE_XML);

            BasicFactory b = new BasicFactory();
            (b as IFactory<BasicTypesSampleWithoutAttr>).Init(src);

            b.BuildInt().Should().Be(42);
            b.BuildString().Should().Be("whatever");
        }

        [Test]
        public void SourcesFactoredTest()
        {
            IConfigSource<BasicTypesSampleWithoutAttr> src =
                new XmlConfigSource<BasicTypesSampleWithoutAttr>().Load(
                XmlConfigSourceFixture.SAMPLE_XML);

            SourceManager.Do.Remove<BasicTypesSampleWithoutAttr>();
            SourceManager.Do.Register(src);

            var b = new BasicFactory();
            SourceManager.Do.AttachFactory(b);

            b.BuildInt().Should().Be(42);
            b.BuildString().Should().Be("whatever");
        }

        [Test]
        public void BestKeyOfTests()
        {
            SourceManager.Do.BestKeyOf(1, SourceManager.Do.DefaultKey, 3).Should().Be(1);
            SourceManager.Do.BestKeyOf(1, null, 3).Should().Be(1);

            SourceManager.Do.BestKeyOf(null, 2, 3).Should().Be(2);
            SourceManager.Do.BestKeyOf(SourceManager.Do.DefaultKey, 2, 3).Should().Be(2);

            SourceManager.Do.BestKeyOf(null, SourceManager.Do.DefaultKey, 3).Should().Be(3);
            SourceManager.Do.BestKeyOf(SourceManager.Do.DefaultKey, null, 3).Should().Be(3);

            SourceManager.Do.BestKeyOf(null, SourceManager.Do.DefaultKey, null).Should().Be(null);
            SourceManager.Do.BestKeyOf(null, null, SourceManager.Do.DefaultKey).Should().Be(SourceManager.Do.DefaultKey);
        }

        [Test]
        public void RedoSourcesFactoredTest()
        {
            IConfigSource<BasicTypesSampleWithoutAttr> src =
                new XmlConfigSource<BasicTypesSampleWithoutAttr>().Load(
                XmlConfigSourceFixture.SAMPLE_XML);

            SourceManager.Do.Remove<BasicTypesSampleWithoutAttr>();

            var b = new BasicFactory();

            SourceManager.Do.AttachFactory(b);
            b.BuildString().Should().Be(default(string));
            b.BuildInt().Should().Be(default(int));

            SourceManager.Do.Register(src);
            b.BuildString().Should().Be("whatever");
            b.BuildInt().Should().Be(42);

            SourceManager.Do.Remove<BasicTypesSampleWithoutAttr>();
            b.BuildString().Should().Be(default(string));
            b.BuildInt().Should().Be(default(int));
        }

        [Test]
        public void RedoSourcesFactoredTestWithKey()
        {
            IConfigSource<BasicTypesSampleWithoutAttr> src =
                new XmlConfigSource<BasicTypesSampleWithoutAttr>().Load(
                XmlConfigSourceFixture.SAMPLE_XML);

            SourceManager.Do.Clear<BasicTypesSampleWithoutAttr>();

            var b = new BasicFactory();

            SourceManager.Do.AttachFactory(2, b);
            b.BuildString().Should().Be(default(string));
            b.BuildInt().Should().Be(default(int));

            SourceManager.Do.Register(2, src);
            b.BuildString().Should().Be("whatever");
            b.BuildInt().Should().Be(42);

            SourceManager.Do.Remove<BasicTypesSampleWithoutAttr>(2);
            b.BuildString().Should().Be(default(string));
            b.BuildInt().Should().Be(default(int));
        }


        [Test]
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
                    b.BuildInt().Should().Be(43);
                    b.BuildString().Should().Be("whatever2");
                    flag = true;
                };

                b.BuildInt().Should().Be(42);

                File.WriteAllText(XmlFileConfigSourceFixture.TEST_FILE_NAME, XmlFileConfigSourceFixture.SAMPLE_XML);

                long time = DateTime.Now.Ticks;
                while (!flag && new TimeSpan(DateTime.Now.Ticks - time).TotalSeconds < 3) ;

                flag.Should().Be.True();
            }
        }

        [Test]
        public void WrappedConfigTest()
        {
            IWrappedConfigSource<BasicTypesSampleWithoutAttr> src = new WrappedConfigSource<BasicTypesSampleWithoutAttr>();
            src.Load(new XmlConfigSource<BasicTypesSampleWithoutAttr>().Load(
                XmlConfigSourceFixture.SAMPLE_XML));

            var b = src.Get();
            XmlConfigSourceFixture.TestCreatedSimpleSample(b);

            src.Load(NullConfigSource<BasicTypesSampleWithoutAttr>.Instance);
            src.Get().Should().Be.Null();
        }

        [Test]
        public void WrappedConfigFactoredTest()
        {
            IWrappedConfigSource<BasicTypesSampleWithoutAttr> src = new WrappedConfigSource<BasicTypesSampleWithoutAttr>();
            src.Load(new XmlConfigSource<BasicTypesSampleWithoutAttr>().Load(
                XmlConfigSourceFixture.SAMPLE_XML));

            SourceManager.Do.Clear<BasicTypesSampleWithoutAttr>();
            SourceManager.Do.Register(src);

            var f = new BasicFactory();
            SourceManager.Do.AttachFactory(f);
            f.BuildString().Should().Be("whatever");
            f.BuildInt().Should().Be(42);

            src.Load(NullConfigSource<BasicTypesSampleWithoutAttr>.Instance);
            f.BuildString().Should().Be(default(string));
            f.BuildInt().Should().Be(default(int));
        }

        [Test]
        public void WrappedFactoryConfigTest()
        {
            IWrappedConfigSource<BasicTypesSampleWithoutAttr> src = new WrappedConfigSource<BasicTypesSampleWithoutAttr>();
            src.Load(new XmlConfigSource<BasicTypesSampleWithoutAttr>().Load(
                XmlConfigSourceFixture.SAMPLE_XML));

            FactoryManager<BasicFactory, BasicTypesSampleWithoutAttr> factories = 
                new FactoryManager<BasicFactory, BasicTypesSampleWithoutAttr>(() => new BasicFactory());

            var f = factories.SafeGet();
            f.BuildString().Should().Be(default(string));
            f.BuildInt().Should().Be(default(int));

            SourceManager.Do.Register(src);
            f.BuildString().Should().Be("whatever");
            f.BuildInt().Should().Be(42);
        }

        [Test]
        public void WrappedKeyedFactoryConfigTest()
        {
            IWrappedConfigSource<BasicTypesSampleWithoutAttr> src = new WrappedConfigSource<BasicTypesSampleWithoutAttr>();
            src.Load(new XmlConfigSource<BasicTypesSampleWithoutAttr>().Load(
                XmlConfigSourceFixture.SAMPLE_XML));

            FactoryManager<BasicFactory, BasicTypesSampleWithoutAttr> factories =
                new FactoryManager<BasicFactory, BasicTypesSampleWithoutAttr>(() => new BasicFactory());

            object key = new object();

            var f = factories[key];
            f.BuildString().Should().Be(default(string));
            f.BuildInt().Should().Be(default(int));

            SourceManager.Do.Register(key, src);
            f.BuildString().Should().Be("whatever");
            f.BuildInt().Should().Be(42);
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
