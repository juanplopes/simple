using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Simple.Tests.Experiences
{
    [TestFixture]
    public class ReflectionFixture
    {
        [Test]
        public void DifferentTypesWithTheSameGuid()
        {
            Guid guid1 = typeof(IList<>).GUID;
            Guid guid2 = typeof(IList<int>).GUID;
            Guid guid3 = typeof(IList<string>).GUID;

            Assert.AreEqual(guid1, guid2);
            Assert.AreEqual(guid2, guid3);
            Assert.AreEqual(guid1, guid3);
        }

        [Test]
        public void WhichPropertyIsUnique()
        {
            int hash1 = typeof(IList<>).GetHashCode();
            int hash2 = typeof(IList<int>).GetHashCode();
            int hash3 = typeof(IList<string>).GetHashCode();

            Assert.AreNotEqual(hash1, hash2);
            Assert.AreNotEqual(hash2, hash3);
            Assert.AreNotEqual(hash1, hash3);
        }
    }
}
