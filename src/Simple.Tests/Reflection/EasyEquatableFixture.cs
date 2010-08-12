using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Reflection;
using NUnit.Framework;
using SharpTestsEx;

namespace Simple.Tests.Reflection
{
    public class EasyEquatableFixture
    {
        public class Sample1 : EasyEquatable
        {
            public int IntProp { get; set; }
            public string StringProp { get; set; }
            public DateTime IgnoreProp { get; set; }

            public override EqualityHelper CreateHelper()
            {
                return new EqualityHelper<Sample1>().Add(x => x.IntProp).Add(x => x.StringProp);
            }
        }

        [Test]
        public void CanCompareEqualInstances()
        {
            var obj1 = new Sample1() { IntProp = 1, StringProp = "2", IgnoreProp = DateTime.Now };
            var obj2 = new Sample1() { IntProp = 1, StringProp = "2", IgnoreProp = DateTime.Now.AddDays(1) };

            obj2.Should().Be(obj1);
            obj2.GetHashCode().Should().Be(obj1.GetHashCode());
        }

        [Test]
        public void CanCompareDifferentInstances()
        {
            var obj1 = new Sample1() { IntProp = 1, StringProp = "2", IgnoreProp = DateTime.Now };
            var obj2 = new Sample1() { IntProp = 1, StringProp = "3", IgnoreProp = DateTime.Now.AddDays(1) };

            obj2.Should().Not.Be(obj1);
            obj2.GetHashCode().Should().Not.Be(obj1.GetHashCode());
        }
    }
}
