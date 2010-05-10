using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;
using Simple.IO;

namespace Simple.Tests.IO
{
    [TestFixture]
    public class StringUtilsFixture
    {
        [Test]
        public void TestRemovePortugueseDiacritics()
        {
            Assert.AreEqual("cao", StringUtils.RemoveDiacritics("cão"));
            Assert.AreEqual("acao", StringUtils.RemoveDiacritics("ação"));
            Assert.AreEqual("aeiouaeiouao", StringUtils.RemoveDiacritics("áéíóúâêîôûãõ"));
        }

        [Test]
        public void TestWontRemoveSpaces()
        {
            Assert.AreEqual("cao sarnento", StringUtils.RemoveDiacritics("cão sarnento"));
            Assert.AreEqual("acao e aventura", StringUtils.RemoveDiacritics("ação e aventura"));
            Assert.AreEqual("aeiou aeiou ao", StringUtils.RemoveDiacritics("áéíóú âêîôû ãõ"));
        }

        [Test]
        public void TestRemoveCrazyDiacritics()
        {
            Assert.AreEqual("aEn", StringUtils.RemoveDiacritics("áÈñ"));
            Assert.AreEqual("uayCu", StringUtils.RemoveDiacritics("üåÿĈǜ"));
        }

        [Test]
        public void TestSplitSimpleIdValueString()
        {
            var res = StringUtils.Split("12 - algum texto", x=>Regex.IsMatch(x.ToString(), "[a-z0-9]")).ToArray();

            Assert.AreEqual(3, res.Length);
            CollectionAssert.Contains(res, "12");
            CollectionAssert.Contains(res, "algum");
            CollectionAssert.Contains(res, "texto");
        }

        [Test]
        public void TestSplitCrazyValues()
        {
            var res = StringUtils.Split("aà:[]b", "[a-z0-9]").ToArray();

            Assert.AreEqual(2, res.Length);
            CollectionAssert.Contains(res, "a");
            CollectionAssert.Contains(res, "b");
        }
    }
}
