using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using NUnit.Framework;
using SharpTestsEx;
using System.Text.RegularExpressions;

namespace Simple.Tests.Generator
{
    public class RegexHelperFixture
    {
        [Test]
        public void CanCorrectDoubleSpacedStrings()
        {
            "asd      qwe".CorrectInput().Should().Be("asd qwe");
        }

        [Test]
        public void CanCorrectNonSpacedNonWordMarkAtStart()
        {
            "asd qwe(test)".CorrectInput().Should().Be("asd qwe (test)");
        }

        [Test]
        public void CanCorrectNonSpacedNonWordMarkAtEnd()
        {
            "asd qwe(test)zxc".CorrectInput().Should().Be("asd qwe (test) zxc");
        }

    }
}
