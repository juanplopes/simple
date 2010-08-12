using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SharpTestsEx;
using Simple.Generator.Parsers;
using System.Text.RegularExpressions;

namespace Simple.Tests.Generator
{
    class RegexesFixture
    {
        [Test]
        public void ListRegexConsiderSpacings()
        {
            AssertListRegex(new[] { "asd", "qwe", "zxc" }, "asd  ,qwe,   zxc");
            AssertListRegex(new[] { "asd", "qwe", "zxc" }, "   ( asd  ,qwe,   zxc )   ");
            AssertListRegex(new[] { "asd", "qwe", "zxc" }, "( asd  ,qwe,   zxc )");
        }

        [Test]
        public void ListRegexDoesntMatchWrongSentences()
        {
            AssertListRegex(new string[0], "'   ( asd  ,qwe,   zxc )   ");
        }

        [Test]
        public void ListRegexMatchSingleValues()
        {
            AssertListRegex(new[] { "asd" }, "asd");
        }

        [Test]
        public void ListRegexConsiderSpecialChars()
        {
            AssertListRegex(new[] { "a+sd", "q@we", "z^xc" }, "a+sd  ,q@we,   z^xc");
        }

        [Test]
        public void ListRegexConsiderQuotes()
        {
            AssertListRegex(new[] { "a sd", "qw e ", "   z^xc   " }, "\"a sd\"  ,'qw e ',   \"   z^xc   \"");
        }

        [Test]
        public void ListRegexMatchesEvenEmptyStrings()
        {
            AssertListRegex(new[] { "", "", "" }, "\"\"  ,'',   \"\"");
        }

        [Test]
        public void OptionRegexMatchesBooleanShorthands()
        {
            AssertOptionRegex(new[] { "+" }, "+option");
            AssertOptionRegex(new[] { "-" }, "-option");
            AssertOptionRegex(new string[] { }, "&option");
        }

        [Test]
        public void OptionRegexMatchesBooleanNormal()
        {
            AssertOptionRegex(new[] { "+" }, "option +");
            AssertOptionRegex(new[] { "-" }, "option    -");
            AssertOptionRegex(new[] { "+", "-" }, "option    (+, -)");
        }


        [Test]
        public void OptionRegexMatchesStringLists()
        {
            AssertOptionRegex(new[] { "asd", "qwe qwe", "@test" }, "option    asd, 'qwe qwe', @test");
        }

        [Test]
        public void OptionRegexDoesntMatchDoubleBoolean()
        {
            AssertOptionRegex(new[] { "+" }, "+option asd");
        }



        [Test]
        public void ListRegexFormatDoestItsWorkEvenInToughCases()
        {
            AssertListRegex(new[] { "a+sd", "q+we", "z+xc" }, "a+sd  ,q+we,   z+xc");
            AssertListRegex(new[] { "asd", "qwe", "zxc" }, "( asd  ,qwe,   zxc )");
            AssertListRegex(new[] { "asd", "qwe", "zxc" }, "   ( asd  ,qwe,   zxc )   ");
            AssertListRegex(new string[0], "'   ( asd  ,qwe,   zxc )   ");
            AssertListRegex(new[] { "asd" }, "asd");
        }

        private static void AssertListRegex(string[] array, string str)
        {
            CollectionAssert.AreEqual(array,
                Regexes.ListRegex.Match(str).Groups[Regexes.ValueGroup].Captures.OfType<Capture>().Select(x => x.Value).ToArray());
        }

        private static void AssertOptionRegex(string[] array, string str)
        {
            CollectionAssert.AreEqual(array,
                Regexes.OptionRegex("option").Match(str).Groups[Regexes.ValueGroup].Captures.OfType<Capture>().Select(x => x.Value).ToArray());
        }

    }
}
