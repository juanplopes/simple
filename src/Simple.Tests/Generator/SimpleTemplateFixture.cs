using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.NVelocity;
using NUnit.Framework;
using SharpTestsEx;

namespace Simple.Tests.Generator
{
    public class SimpleTemplateFixture
    {
        [Test]
        public void CanReplaceSimpleValue()
        {
            var t = new SimpleTemplate("asd${asd}asd");
            t["asd"] = 123;
            t.Render().Should().Be("asd123asd");
        }

        [Test]
        public void CanReplaceSimpleValueUsingToString()
        {
            var t = new SimpleTemplate("asd${asd}asd");
            t["asd"] = 123;
            t.ToString().Should().Be("asd123asd");
        }

        [Test]
        public void CanReplaceSimpleValueUsingAnonymousObject()
        {
            var t = new SimpleTemplate("asd${asd}asd$qwe");

            t.SetMany(new { asd = 123, qwe = 456 });

            t.ToString().Should().Be("asd123asd456");
        }


        [Test]
        public void CanReplaceSimpleValueUsingExpressions()
        {
            var t = new SimpleTemplate("asd${asd}asd$qwe");

            t.SetMany(asd => 123, qwe => 456);

            t.ToString().Should().Be("asd123asd456");
        }
    }
}
