using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using NUnit.Framework;
using System.Text.RegularExpressions;

namespace Simple.Tests.Generator
{
    public class RegexHelperFixture
    {
        [Test]
        public void ListRegexFormatDoestItsWork()
        {
            AssertRegex(new[] { "asd", "qwe", "zxc" }, "asd  ,qwe,   zxc");
            AssertRegex(new[] { "asd", "qwe", "zxc" }, "( asd  ,qwe,   zxc )");
            AssertRegex(new[] { "asd", "qwe", "zxc" }, "   ( asd  ,qwe,   zxc )   ");
            AssertRegex(new string[0], ";;;   ( asd  ,qwe,   zxc )   ");
            AssertRegex(new[] { "asd" }, "asd");
        }

        private static void AssertRegex(string[] array, string str)
        {
            CollectionAssert.AreEqual(array,
                RegexHelper.ListRegex.Match(str).Groups[RegexHelper.ValueGroup].Captures.OfType<Capture>().Select(x => x.Value).ToArray());
        }


        [Test]
        public void CanCorrectDoubleSpacedStrings()
        {
            Assert.AreEqual("asd qwe", "asd      qwe".CorrectInput());
        }

        [Test]
        public void CanCorrectNonSpacedNonWordMarkAtStart()
        {
            Assert.AreEqual("asd qwe (test)", "asd qwe(test)".CorrectInput());
        }

        [Test]
        public void CanCorrectNonSpacedNonWordMarkAtEnd()
        {
            Assert.AreEqual("asd qwe (test) zxc", "asd qwe(test)zxc".CorrectInput());
        }

    }
}
