using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NHibernate.Criterion;
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
        public void CreateSimpleObjectFromExpressions()
        {
            var value = DictionaryHelper.FromExpressions(aaa => 123, bbb => "asd");

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

        [Test]
        public void CreateSimpleObjectFromExpressionsWithNullableValues()
        {
            var value = DictionaryHelper.FromExpressions(aaa => null, bbb => null);

            Assert.IsInstanceOf<IDictionary<string, object>>(value);
            Assert.AreEqual(2, value.Count);
            Assert.AreEqual(null, value["aaa"]);
            Assert.AreEqual(null, value["bbb"]);
        }

        [Test]
        public void CreateSimpleObjectFromAnonymousTypesCaseSensitive()
        {
            var value = DictionaryHelper.FromAnonymous(new { aaa = 123, bbb = "asd" }, true);

            Assert.IsInstanceOf<IDictionary<string, object>>(value);
            Assert.AreEqual(2, value.Count);

            Assert.IsTrue(value.ContainsKey("aaa"));
            Assert.IsFalse(value.ContainsKey("Aaa"));

            Assert.IsTrue(value.ContainsKey("bbb"));
            Assert.IsFalse(value.ContainsKey("Bbb"));
        }

        [Test]
        public void CreateSimpleObjectFromExpressionsCaseSensitive()
        {
            Expression<Func<object, object>>[] array = new Expression<Func<object, object>>[] { 
                aaa => 123, bbb => "asd"};

            var value = DictionaryHelper.FromExpressions(array, true);

            Assert.IsInstanceOf<IDictionary<string, object>>(value);
            Assert.AreEqual(2, value.Count);

            Assert.IsTrue(value.ContainsKey("aaa"));
            Assert.IsFalse(value.ContainsKey("Aaa"));

            Assert.IsTrue(value.ContainsKey("bbb"));
            Assert.IsFalse(value.ContainsKey("Bbb"));
        }

        [Test]
        public void CreateSimpleObjectFromAnonymousTypesCaseInsensitive()
        {
            var value = DictionaryHelper.FromAnonymous(new { aaa = 123, bbb = "asd" });

            Assert.IsInstanceOf<IDictionary<string, object>>(value);
            Assert.AreEqual(2, value.Count);

            Assert.IsTrue(value.ContainsKey("aaa"));
            Assert.IsTrue(value.ContainsKey("Aaa"));

            Assert.IsTrue(value.ContainsKey("bbb"));
            Assert.IsTrue(value.ContainsKey("Bbb"));
        }

        [Test]
        public void CreateSimpleObjectFromExpressionsCaseInsensitive()
        {
            var value = DictionaryHelper.FromExpressions(aaa => 123, bbb => "asd");

            Assert.IsInstanceOf<IDictionary<string, object>>(value);
            Assert.AreEqual(2, value.Count);

            Assert.IsTrue(value.ContainsKey("aaa"));
            Assert.IsTrue(value.ContainsKey("Aaa"));

            Assert.IsTrue(value.ContainsKey("bbb"));
            Assert.IsTrue(value.ContainsKey("Bbb"));
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

        [Test]
        public void CreateSimpleObjectFromNullExpressions()
        {
            var value = DictionaryHelper.FromExpressions(null);

            Assert.IsInstanceOf<IDictionary<string, object>>(value);
            Assert.AreEqual(0, value.Count);
        }

        [Test]
        public void CreateSimpleObjectFromNullObject()
        {
            var value = DictionaryHelper.FromAnonymous(null);

            Assert.IsInstanceOf<IDictionary<string, object>>(value);
            Assert.AreEqual(0, value.Count);
        }


    }
}
