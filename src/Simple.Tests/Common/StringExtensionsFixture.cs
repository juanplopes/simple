using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;
using SharpTestsEx;
using Simple.IO;

namespace Simple.Tests.Common
{
    [TestFixture]
    public class StringUtilsFixture
    {
        [Test]
        public void TestRemovePortugueseDiacritics()
        {
            "cão".RemoveDiacritics().Should().Be("cao");
            "ação".RemoveDiacritics().Should().Be("acao");
            "áéíóúâêîôûãõ".RemoveDiacritics().Should().Be("aeiouaeiouao");
        }

        [Test]
        public void TestWontRemoveSpaces()
        {
            "cão sarnento".RemoveDiacritics().Should().Be("cao sarnento");
            "ação e aventura".RemoveDiacritics().Should().Be("acao e aventura");
            "áéíóú âêîôû ãõ".RemoveDiacritics().Should().Be("aeiou aeiou ao");
        }

        [Test]
        public void TestRemoveCrazyDiacritics()
        {
            "áÈñ".RemoveDiacritics().Should().Be("aEn");
            "üåÿĈǜ".RemoveDiacritics().Should().Be("uayCu");
        }

        [Test]
        public void TestSplitSimpleIdValueString()
        {
            var res = "12 - algum texto".Split(x=>Regex.IsMatch(x.ToString(), "[a-z0-9]")).ToArray();

            res.Length.Should().Be(3);
            CollectionAssert.Contains(res, "12");
            CollectionAssert.Contains(res, "algum");
            CollectionAssert.Contains(res, "texto");
        }

        [Test]
        public void TestSplitCrazyValues()
        {
            var res = "aà:[]b".Split("[a-z0-9]").ToArray();

            res.Length.Should().Be(2);
            CollectionAssert.Contains(res, "a");
            CollectionAssert.Contains(res, "b");
        }
    }
}
