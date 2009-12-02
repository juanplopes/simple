using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.IO.Serialization;

namespace Simple.Tests.Serialization
{
    public abstract class BaseSerializerFixture
    {
        protected abstract ISimpleSerializer GetSerializer<T>();
        protected abstract ISimpleStringSerializer GetStringSerializer<T>();

        [Serializable]
        public class Sample1
        {
            public int Prop1 { get; set; }
            public string Prop2 { get; set; }
        }

        [Test]
        public void TestSerialize()
        {
            var value = new Sample1() { Prop1 = 2, Prop2 = "333" };
            var data = GetSerializer<Sample1>().Serialize(value);

            var value2 = GetSerializer<Sample1>().Deserialize(data);
            var value3 = value2 as Sample1;
            Assert.IsInstanceOf<Sample1>(value2);
            Assert.AreNotEqual(value, value3);
            Assert.AreEqual(value.Prop1, value3.Prop1);
            Assert.AreEqual(value.Prop2, value3.Prop2);
        }
        [Test]
        public void TestSerializeStruct()
        {
            var value = 123;
            var data = GetSerializer<int>().Serialize(value);

            var value2 = GetSerializer<int>().Deserialize(data);
            var value3 = (int)value2;
            Assert.IsInstanceOf<int>(value2);
            Assert.AreEqual(value, value3);
        }

        [Test]
        public void TestSerializeNullValueWithType()
        {
            Sample1 value = null;
            var data = GetSerializer<Sample1>().Serialize(value);

            var value2 = GetSerializer<Sample1>().Deserialize(data);
            var value3 = (Sample1)value2;
            Assert.IsNull(value2);
            Assert.AreEqual(value, value3);
        }

        [Test]
        public void TestSerializeEmptyObject()
        {
            object value = new object();
            var data = GetSerializer<object>().Serialize(value);

            var value2 = GetSerializer<object>().Deserialize(data);
            Assert.IsInstanceOf<object>(value2);
        }


        [Test]
        public void TestSerializeToString()
        {
            var value = new Sample1() { Prop1 = 2, Prop2 = "333" };
            var data = GetSerializer<Sample1>().Serialize(value);
            var data2 = GetStringSerializer<Sample1>().SerializeToString(value);
        }

        [Test]
        public void TestSerializeFromString()
        {
            var value = 123;
            var data = GetStringSerializer<int>().SerializeToString(value);

            var value2 = GetStringSerializer<int>().DeserializeFromString(data);
            var value3 = (int)value2;
            Assert.IsInstanceOf<int>(value2);
            Assert.AreEqual(value, value3);
        }
    }
}
