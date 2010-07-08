using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;
using Simple.IO;

namespace Simple.Tests.Common
{
    [TestFixture]
    public class StringUtilsFixture
    {
        [Test]
        public void TestRemovePortugueseDiacritics()
        {
            Assert.AreEqual("cao", "cão".RemoveDiacritics());
            Assert.AreEqual("acao", "ação".RemoveDiacritics());
            Assert.AreEqual("aeiouaeiouao", "áéíóúâêîôûãõ".RemoveDiacritics());
        }

        [Test]
        public void TestWontRemoveSpaces()
        {
            Assert.AreEqual("cao sarnento", "cão sarnento".RemoveDiacritics());
            Assert.AreEqual("acao e aventura", "ação e aventura".RemoveDiacritics());
            Assert.AreEqual("aeiou aeiou ao", "áéíóú âêîôû ãõ".RemoveDiacritics());
        }

        [Test]
        public void TestRemoveCrazyDiacritics()
        {
            Assert.AreEqual("aEn", "áÈñ".RemoveDiacritics());
            Assert.AreEqual("uayCu", "üåÿĈǜ".RemoveDiacritics());
        }

        [Test]
        public void TestSplitSimpleIdValueString()
        {
            var res = "12 - algum texto".Split(x=>Regex.IsMatch(x.ToString(), "[a-z0-9]")).ToArray();

            Assert.AreEqual(3, res.Length);
            CollectionAssert.Contains(res, "12");
            CollectionAssert.Contains(res, "algum");
            CollectionAssert.Contains(res, "texto");
        }

        [Test]
        public void TestSplitCrazyValues()
        {
            var res = "aà:[]b".Split("[a-z0-9]").ToArray();

            Assert.AreEqual(2, res.Length);
            CollectionAssert.Contains(res, "a");
            CollectionAssert.Contains(res, "b");
        }
    }
}
