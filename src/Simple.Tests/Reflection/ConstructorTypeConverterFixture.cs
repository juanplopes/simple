using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.ComponentModel;
using Simple.Reflection;
using SharpTestsEx;

namespace Simple.Tests.Reflection
{
    [TestFixture]
    public class ConstructorTypeConverterFixture
    {
        [Test]
        public void CanConvertFromSampleInt()
        {
            var converter = TypeDescriptor.GetConverter(typeof(SampleIntOnly));
            converter.ConvertFrom(42).Should().Be.OfType<SampleIntOnly>()
                .And.Value.Value.Should().Be(42);
        }

        [Test]
        public void CanConvertFromSampleString()
        {
            var converter = TypeDescriptor.GetConverter(typeof(SampleIntOnly));
            converter.ConvertFrom("42").Should().Be.OfType<SampleIntOnly>()
                .And.Value.Value.Should().Be(42);
        }

        [TypeConverter(typeof(ConstructorTypeConverter<SampleIntOnly>))]
        public class SampleIntOnly
        {
            public int Value;
            public SampleIntOnly(int value) { this.Value = value; }
        }
    }
}
