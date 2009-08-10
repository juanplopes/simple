using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Simple.Common;

namespace Simple.Tests.SimpleLib
{
    [TestFixture]
    public class MathHelperFixture
    {
        [Test]
        public void FirstPrimes()
        {
            List<int> primes = new List<int>(PrimeNumbers.GetPrimesEnumerable());
            Assert.AreEqual(2, primes[0]);
            Assert.AreEqual(3, primes[1]);
            Assert.AreEqual(5, primes[2]);
            Assert.AreEqual(7, primes[3]);
            Assert.AreEqual(11, primes[4]);
        }

        [Test]
        public void ModRound()
        {
            Assert.AreEqual(8, MathHelper.ModRound(10, 4));
            Assert.AreEqual(9, MathHelper.ModRound(10, 3));
            Assert.AreEqual(100, MathHelper.ModRound(106, 100));

            Assert.AreEqual(8L, MathHelper.ModRound(10L, 4L));
            Assert.AreEqual(9L, MathHelper.ModRound(10L, 3L));
            Assert.AreEqual(100L, MathHelper.ModRound(106L, 100L));
        }
    }
}
