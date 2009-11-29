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
            List<int> primes = new List<int>(MathHelper.GetPrimesEnumerable(12));
            Assert.AreEqual(5, primes.Count);
            
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

        [Test]
        public void NegativeModRound()
        {
            Assert.AreEqual(-12, MathHelper.ModRound(-10, 4));
            Assert.AreEqual(-12, MathHelper.ModRound(-10, 3));
            Assert.AreEqual(-200, MathHelper.ModRound(-106, 100));

            Assert.AreEqual(-12L, MathHelper.ModRound(-10L, 4L));
            Assert.AreEqual(-12L, MathHelper.ModRound(-10L, 3L));
            Assert.AreEqual(-200L, MathHelper.ModRound(-106L, 100L));
        }

        [Test]
        public void TestRealMod()
        {
            Assert.AreEqual(2, MathHelper.RealMod(10, 4));
            Assert.AreEqual(2, MathHelper.RealMod(-10, 4));

            Assert.AreEqual(1, MathHelper.RealMod(10, 3));
            Assert.AreEqual(2, MathHelper.RealMod(-10, 3));

            Assert.AreEqual(6, MathHelper.RealMod(106, 100));
            Assert.AreEqual(94, MathHelper.RealMod(-106, 100));
        }

        [Test]
        public void TestFloatEqTests()
        {
            Assert.IsTrue(MathHelper.FloatEq(10.099m, 10m, 0.1m));
            Assert.IsFalse(MathHelper.FloatEq(10.1m, 10m, 0.1m));

            Assert.IsTrue(MathHelper.FloatEq(10.099, 10, 0.1));
        }
    }
}
