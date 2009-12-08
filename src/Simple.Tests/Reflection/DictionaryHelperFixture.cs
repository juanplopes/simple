using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Reflection;

namespace Simple.Tests.Reflection
{
    [TestFixture]
    public class DictionaryHelperFixture
    {
        [Test]
        public void CreateSimpleObjectFromAnonymousTypes()
        {
            var value = DictionaryHelper.FromAnonymous(new { aaa = 123, bbb = "asd" });

            Assert.IsInstanceOf<IDictionary<string, object>>(value);
            Assert.AreEqual(2, value.Count);
            Assert.AreEqual(123, value["aaa"]);
            Assert.AreEqual("asd", value["bbb"]);
        }

        [Test]
        public void CreateSimpleObjectFromAnonymousTypesWithNullableValues()
        {
            var value = DictionaryHelper.FromAnonymous(new { aaa = (int?)null, bbb = (string)null });

            Assert.IsInstanceOf<IDictionary<string, object>>(value);
            Assert.AreEqual(2, value.Count);
            Assert.AreEqual(null, value["aaa"]);
            Assert.AreEqual(null, value["bbb"]);
        }

        class Sample1
        {
            public DateTime AAA { get; set; }
            public string BBB { get; set; }
        }
        [Test]
        public void CreateSimpleObjectFromExistingClass()
        {
            var now = DateTime.Now;
            var obj = new Sample1() { AAA = now, BBB = "ASD" };
            var value = DictionaryHelper.FromAnonymous(obj);

            Assert.IsInstanceOf<IDictionary<string, object>>(value);
            Assert.AreEqual(2, value.Count);
            Assert.AreEqual(now, value["AAA"]);
            Assert.AreEqual("ASD", value["BBB"]);
        }

    }
}
