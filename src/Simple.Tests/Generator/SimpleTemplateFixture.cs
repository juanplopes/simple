using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.NVelocity;
using NUnit.Framework;

namespace Simple.Tests.Generator
{
    public class SimpleTemplateFixture
    {
        [Test]
        public void CanReplaceSimpleValue()
        {
            var t = new SimpleTemplate("asd${asd}asd");
            t["asd"] = 123;
            Assert.AreEqual("asd123asd", t.Render());
        }

        [Test]
        public void CanReplaceSimpleValueUsingToString()
        {
            var t = new SimpleTemplate("asd${asd}asd");
            t["asd"] = 123;
            Assert.AreEqual("asd123asd", t.ToString());
        }
    }
}
