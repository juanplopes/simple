using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;
using Simple.ConfigSource;

namespace Simple.Tests.SimpleLib
{
    [TestFixture]
    public class XmlFileConfigSourceFixture
    {
        public const string TEST_FILE_NAME = "__test__.xml";

        public static string SAMPLE_XML =
            @"<BasicTypesSampleWithoutAttr>
                    <AString>whatever2</AString>
                    <AnIntegral>43</AnIntegral>
                    <AFloat>43.43</AFloat>
                  </BasicTypesSampleWithoutAttr>";


        [SetUp]
        public void Setup()
        {
            File.WriteAllText(TEST_FILE_NAME, XmlConfigSourceFixture.SAMPLE_XML);
        }

        [TearDown]
        public void Teardown()
        {
            if (File.Exists(TEST_FILE_NAME))
                File.Delete(TEST_FILE_NAME);
        }


        [Test]
        public void SimpleLoadTest()
        {
            using (var src = new XmlFileConfigSource<BasicTypesSampleWithoutAttr>())
            {
                var cfg = src.Load(new FileInfo(TEST_FILE_NAME)).Get();
                XmlConfigSourceFixture.TestCreatedSimpleSample(cfg);
            }
        }

        //[Test]
        //public void WithRepositoryTest()
        //{
        //    using (var src = new XmlFileConfigSource<BasicTypesSampleWithoutAttr>())
        //    {
        //        ConfigRepository conf = new ConfigRepository();
        //        conf.SetSource(src.LoadFile(TEST_FILE_NAME));
        //        XmlConfigSourceFixture.TestCreatedSimpleSample(conf.Get<BasicTypesSampleWithoutAttr>());
        //    }
        //}

        [Test]
        public void SimpleLoadAndModifyTest()
        {
            using (var src = new XmlFileConfigSource<BasicTypesSampleWithoutAttr>()
                .LoadFile(TEST_FILE_NAME))
            {
                bool flag = false;
                var cfg = src.Get();

                src.Reloaded += x =>
                {
                    cfg = x;
                    Assert.AreEqual(43, cfg.AnIntegral);
                    Assert.AreEqual(43.43, cfg.AFloat, 0.001);
                    flag = true;
                };

                XmlConfigSourceFixture.TestCreatedSimpleSample(cfg);

                File.WriteAllText(TEST_FILE_NAME, SAMPLE_XML);

                long time = DateTime.Now.Ticks;
                while (!flag && new TimeSpan(DateTime.Now.Ticks - time).TotalSeconds < 3) ;

                Assert.IsTrue(flag);
            }

            
        }
    }
}
