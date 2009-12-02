using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Reflection;

namespace Simple.Tests.Reflection
{
    [TestFixture]
    public class UltraCasterFixture
    {
        class SampleWithImplicit
        {
            public string Value { get; set; }

            public static implicit operator int(SampleWithImplicit value)
            {
                return int.Parse(value.Value + "2");
            }
        }

        class SampleWithExplicit
        {
            public string Value { get; set; }

            public static explicit operator int(SampleWithExplicit value)
            {
                return int.Parse(value.Value + "3");
            }
        }

        class SampleWithImplicitOther1
        {
            public string Value { get; set; }

            public static implicit operator SampleWithImplicitOther2(SampleWithImplicitOther1 value)
            {
                return new SampleWithImplicitOther2() { Value = value.Value + "91" };
            }
        }

        class SampleWithImplicitOther2
        {
            public string Value { get; set; }

            public static implicit operator SampleWithImplicitOther1(SampleWithImplicitOther2 value)
            {
                return new SampleWithImplicitOther1() { Value = value.Value + "92" };
            }

            public static implicit operator SampleWithImplicitOther2(SampleWithImplicitOther1 value)
            {
                return new SampleWithImplicitOther2() { Value = value.Value + "82" };
            }
        }

        class SampleWithImplicitOther3
        {
            public string Value { get; set; }

            public static implicit operator SampleWithImplicitOther3(SampleWithImplicitOther1 value)
            {
                return new SampleWithImplicitOther3() { Value = value.Value + "83" };
            }
        }

        [Test]
        public void CanConvertStringToInt()
        {
            Assert.AreEqual(42, UltraCaster.TryCast("42", typeof(int)));
        }

        [Test]
        public void CanConvertIntToFloat()
        {
            Assert.IsInstanceOf<float>(UltraCaster.TryCast(42, typeof(float)));
        }

        [Test]
        public void CanConvertUsingImplicitConversion()
        {
            var value = new SampleWithImplicit() { Value = "42"};
            Assert.AreEqual(422, UltraCaster.TryCast(value, typeof(int)));
        }
        [Test]
        public void CanConvertUsingExplicitConversion()
        {
            var value = new SampleWithExplicit() { Value = "42" };
            Assert.AreEqual(423, UltraCaster.TryCast(value, typeof(int)));
        }

        [Test]
        public void CanConvertPreferingFromConversion()
        {
            var value = new SampleWithImplicitOther1() { Value = "42" };
            var res = (SampleWithImplicitOther2)UltraCaster.TryCast(value, typeof(SampleWithImplicitOther2));
            Assert.AreEqual("4291", res.Value);
        }
        [Test]
        public void CanConvertPreferingFromConversionInverse()
        {
            var value = new SampleWithImplicitOther2() { Value = "42" };
            var res = (SampleWithImplicitOther1)UltraCaster.TryCast(value, typeof(SampleWithImplicitOther1));
            Assert.AreEqual("4292", res.Value);
        }
        [Test]
        public void CanConvertPreferingToConversion()
        {
            var value = new SampleWithImplicitOther1() { Value = "42" };
            var res = (SampleWithImplicitOther3)UltraCaster.TryCast(value, typeof(SampleWithImplicitOther3));
            Assert.AreEqual("4283", res.Value);
        }

    }
}
