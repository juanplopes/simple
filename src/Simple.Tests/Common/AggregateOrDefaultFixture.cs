using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SharpTestsEx;

namespace Simple.Tests.Common
{
    [TestFixture]
    public class AggregateOrDefaultFixture
    {
        [Test]
        public void CanMaxOrDefaultStringValuesWithNullsInTheMiddle()
        {
            var array = new string[] { "1", null, "3", "2" };
            array.MaxOrDefault("a").Should().Be("3");
        }

        [Test]
        public void CanMinOrDefaultStringValuesWithNullsInTheMiddle()
        {
            var array = new string[] { "1", null, "3", "2" };
            array.MinOrDefault("a").Should().Be("1");
        }

        [Test]
        public void CanMaxOrDefaultIntegerValues()
        {
            var array = new int[] { 1, 2, 3, 2 };
            array.MaxOrDefault(50).Should().Be(3);
        }

        [Test]
        public void CanMaxOrDefaultNullableIntegerValues()
        {
            var array = new int?[] { 1, 2, 3, 2 };
            array.MaxOrDefault(50).Should().Be(3);
        }

        [Test]
        public void CanMaxOrDefaultEmptyIntegerValues()
        {
            var array = new int[] { };
            array.MaxOrDefault(50).Should().Be(50);
        }

        [Test]
        public void CanMaxOrDefaultEmptyNullableIntegerValues()
        {
            var array = new int?[] { };
            array.MaxOrDefault(50).Should().Be(50);
        }

        [Test]
        public void CanMaxOrDefaultEmptyIntegerValuesWithoutParameters()
        {
            var array = new int[] { };
            array.MaxOrDefault().Should().Be(0);
        }

        [Test]
        public void CanMaxOrDefaultEmptyNullableIntegerValuessWithoutParameters()
        {
            var array = new int?[] { };
            array.MaxOrDefault().Should().Be(0);
        }

        [Test]
        public void CanMinOrDefaultIntegerValues()
        {
            var array = new int[] { 1, 2, 3, 2 };
            array.MinOrDefault(50).Should().Be(1);
        }

        [Test]
        public void CanMinOrDefaultNullableIntegerValues()
        {
            var array = new int?[] { 1, 2, 3, 2 };
            array.MinOrDefault(50).Should().Be(1);
        }

        [Test]
        public void CanMinOrDefaultEmptyIntegerValues()
        {
            var array = new int[] { };
            array.MinOrDefault(50).Should().Be(50);
        }

        [Test]
        public void CanMinOrDefaultEmptyNullableIntegerValues()
        {
            var array = new int?[] { };
            array.MinOrDefault(50).Should().Be(50);
        }

        [Test]
        public void CanMinOrDefaultEmptyIntegerValuesWithoutParameters()
        {
            var array = new int[] { };
            array.MinOrDefault().Should().Be(0);
        }

        [Test]
        public void CanMinOrDefaultEmptyNullableIntegerValuessWithoutParameters()
        {
            var array = new int?[] { };
            array.MinOrDefault().Should().Be(0);
        }

        [Test]
        public void CanSumOrDefaultIntegerValues()
        {
            var array = new int[] { 1, 2, 3, 2 };
            array.SumOrDefault(50).Should().Be(8);
        }

        [Test]
        public void CanSumOrDefaultNullableIntegerValues()
        {
            var array = new int?[] { 1, 2, 3, 2 };
            array.SumOrDefault(50).Should().Be(8);
        }

        [Test]
        public void CanSumOrDefaultEmptyIntegerValues()
        {
            var array = new int[] { };
            array.SumOrDefault(50).Should().Be(50);
        }

        [Test]
        public void CanSumOrDefaultEmptyNullableIntegerValues()
        {
            var array = new int?[] { };
            array.SumOrDefault(50).Should().Be(50);
        }

        [Test]
        public void CanSumOrDefaultEmptyIntegerValuesWithoutParameters()
        {
            var array = new int[] { };
            array.SumOrDefault().Should().Be(0);
        }

        [Test]
        public void CanSumOrDefaultEmptyNullableIntegerValuessWithoutParameters()
        {
            var array = new int?[] { };
            array.SumOrDefault().Should().Be(0);
        }

        [Test]
        public void CanSumOrDefaultShortValues()
        {
            var array = new short[] { 1, 2, 3, 2 };
            array.SumOrDefault(50).Should().Be(8);
        }

        [Test]
        public void CanSumOrDefaultNullableShortValues()
        {
            var array = new short?[] { 1, 2, 3, 2 };
            array.SumOrDefault(50).Should().Be(8);
        }

        [Test]
        public void CanSumOrDefaultEmptyShortValues()
        {
            var array = new short[] { };
            array.SumOrDefault(50).Should().Be(50);
        }

        [Test]
        public void CanSumOrDefaultEmptyNullableShortValues()
        {
            var array = new short?[] { };
            array.SumOrDefault(50).Should().Be(50);
        }

        [Test]
        public void CanSumOrDefaultEmptyShortValuesWithoutParameters()
        {
            var array = new short[] { };
            array.SumOrDefault().Should().Be(0);
        }

        [Test]
        public void CanSumOrDefaultEmptyNullableShortValuessWithoutParameters()
        {
            var array = new short?[] { };
            array.SumOrDefault().Should().Be(0);
        }
    }
}
