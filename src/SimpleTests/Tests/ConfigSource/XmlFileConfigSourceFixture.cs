using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Simple.ConfigSource;
using System.Xml;
using System.Xml.Serialization;

namespace Simple.Tests.ConfigSource
{
    [TestClass]
    public class XmlFileConfigSourceFixture
    {
        public const string TEST_FILE_NAME = "__test__.xml";

        public static string SAMPLE_XML =
            @"<BasicTypesSampleWithoutAttr>
                    <AString>whatever2</AString>
                    <AnIntegral>43</AnIntegral>
                    <AFloat>43.43</AFloat>
                  </BasicTypesSampleWithoutAttr>";


        [TestInitialize]
        public void Setup()
        {
            File.WriteAllText(TEST_FILE_NAME, XmlConfigSourceFixture.SAMPLE_XML);
        }

        [TestCleanup]
        public void Teardown()
        {
            if (File.Exists(TEST_FILE_NAME))
                File.Delete(TEST_FILE_NAME);
        }


        [TestMethod]
        public void SimpleLoadTest()
        {
            using (var src = new XmlFileConfigSource<BasicTypesSampleWithoutAttr>())
            {
                var cfg = src.Load(new FileInfo(TEST_FILE_NAME)).Get();
                XmlConfigSourceFixture.TestCreatedSimpleSample(cfg);
            }
        }

        //[TestMethod]
        //public void WithRepositoryTest()
        //{
        //    using (var src = new XmlFileConfigSource<BasicTypesSampleWithoutAttr>())
        //    {
        //        ConfigRepository conf = new ConfigRepository();
        //        conf.SetSource(src.LoadFile(TEST_FILE_NAME));
        //        XmlConfigSourceFixture.TestCreatedSimpleSample(conf.Get<BasicTypesSampleWithoutAttr>());
        //    }
        //}

        [TestMethod]
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

    #region Samples
    [XmlRoot("test1")]
    public class Test1
    {
        [XmlElement("s")]
        public string  S { get; set; }
        [XmlElement("i")]
        public int I { get; set; }
    }
    #endregion
}
