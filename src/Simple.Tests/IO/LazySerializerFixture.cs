using System;
using System.Runtime.Serialization;
using NUnit.Framework;
using SharpTestsEx;
using Simple.IO.Serialization;

namespace Simple.Tests.IO
{
    public class LazySerializerFixture
    {
        public class NonSerializable
        {
            public string Value { get; set; }
        }

        [Serializable]
        public class SerializeAsString : LazySerializer<NonSerializable, string>
        {
            public SerializeAsString(NonSerializable real) : base(real) { }
            protected SerializeAsString(SerializationInfo info, StreamingContext context) : base(info, context) { }

            protected override NonSerializable TransformToReal(string proxy)
            {
                return new NonSerializable() { Value = proxy };
            }

            protected override string TransformToProxy(NonSerializable real)
            {
                return real.Value;
            }
        }

        [Test]
        public void WhenTheClassIsNotSerializedTheInstanceIsTheSame()
        {
            var value = new NonSerializable() { Value = "asd" };
            var serializable = new SerializeAsString(value);
            var value2 = serializable.Real;

            Assert.AreSame(value, value2);
        }

        [Test]
        public void WhenTheClassIsSerializedTheInstanceIsTheDifferent()
        {
            var value = new NonSerializable() { Value = "asd" };
            var serializable = new SerializeAsString(value);
            var newSerializable = SerializeAndDeserialize(serializable);
            var value2 = newSerializable.Real;

            Assert.AreNotSame(serializable, newSerializable);
            Assert.AreNotSame(value, value2);
        }

        [Test]
        public void WhenTheClassIsSerializedAndInstanceIsNullWontCauseAnyProblem()
        {
            var serializable = new SerializeAsString(null);
            var newSerializable = SerializeAndDeserialize(serializable);
            var value2 = newSerializable.Real;

            Assert.IsNull(value2);
        }

        [Test]
        public void WhenTheClassIsNullTheProxyIsNullAsWell()
        {
            var serializable = new SerializeAsString(null);
            Assert.IsNull(serializable.Proxy);
        }

        [Test]
        public void WhenJustCreatedTheClassTheProxyIsInactive()
        {
            var value = new NonSerializable() { Value = "asd" };
            var serializable = new SerializeAsString(value);

            Assert.IsFalse(serializable.IsProxyActivated);
        }

        [Test]
        public void WhenTheValueIsNullItDoesntSerializeTheProxy()
        {
            var serializable = new SerializeAsString(null);
            var newSerializable = SimpleSerializer.Binary().RoundTrip(serializable);

            Assert.IsFalse(newSerializable.IsProxyActivated);
            Assert.IsFalse(newSerializable.IsRealActivated);
            Assert.IsNull(newSerializable.Real);
            Assert.IsTrue(newSerializable.IsRealActivated);
        }

        [Test]
        public void WhenAccessTheProxyPropertyItBecomesActivated()
        {
            var value = new NonSerializable() { Value = "asd" };
            var serializable = new SerializeAsString(value);
            var proxy = serializable.Proxy;

            Assert.IsNotNull(proxy);
            Assert.IsTrue(serializable.IsRealActivated);
            Assert.IsTrue(serializable.IsProxyActivated);
        }

        [Test]
        public void WhenJustDeserializedTheClassTheRealIsInactive()
        {
            var value = new NonSerializable() { Value = "asd" };
            var serializable = new SerializeAsString(value);
            var newSerializable = SerializeAndDeserialize(serializable);

            Assert.IsFalse(newSerializable.IsRealActivated);
            Assert.IsTrue(serializable.IsProxyActivated);
        }

        [Test]
        public void WhenAccessTheRealPropertyAfterDeserializationItBecomesActivated()
        {
            var value = new NonSerializable() { Value = "asd" };
            var serializable = new SerializeAsString(value);
            var newSerializable = SerializeAndDeserialize(serializable);
            var real = newSerializable.Real;

            Assert.IsNotNull(real);
            Assert.IsTrue(newSerializable.IsRealActivated);
            Assert.IsTrue(serializable.IsProxyActivated);
        }

        [Test]
        public void WhenTheClassIsNullTheProxyIsNullAsWellEvenWhenDeserialized()
        {
            var serializable = new SerializeAsString(null);
            var newSerializable = SerializeAndDeserialize(serializable);

            Assert.IsNull(serializable.Proxy);
            Assert.IsNull(newSerializable.Real);
            Assert.IsNull(newSerializable.Proxy);
        }

        protected T SerializeAndDeserialize<T>(T value)
        {
            return SimpleSerializer.Binary().RoundTrip(value);
        }
    }
}
