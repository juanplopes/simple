using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using NUnit.Framework;

namespace Simple.Tests.Generator
{
    public class RegexHelperFixture
    {
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
