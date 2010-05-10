using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Simple.Common;

namespace Simple.Tests.Common
{
    [TestFixture]
    public class SimpleMathFixture
    {
        [Test]
        public void FirstPrimes()
        {
            List<int> primes = new List<int>(SimpleMath.GetPrimes().Limit(12));
            Assert.AreEqual(5, primes.Count);

            Assert.AreEqual(2, primes[0]);
            Assert.AreEqual(3, primes[1]);
            Assert.AreEqual(5, primes[2]);
            Assert.AreEqual(7, primes[3]);
            Assert.AreEqual(11, primes[4]);
        }

        [Test]
        public void TestPrimeCacheInfo()
        {
            Assert.AreEqual(6542, SimpleMath.GetPrimes().CachedCount);
        }

        [Test]
        public void TestNoNegativePrimes()
        {
            Assert.IsFalse(SimpleMath.GetPrimes().IsPrime(-11));
            Assert.IsFalse(SimpleMath.GetPrimes().IsPrime(-9));
        }

        [Test]
        public void TestPrimeCacheLimits()
        {
            Assert.IsFalse(SimpleMath.GetPrimes().IsPrime(65520));
            Assert.IsTrue(SimpleMath.GetPrimes().IsPrime(65521));
            Assert.IsFalse(SimpleMath.GetPrimes().IsPrime(65522));
        }

        [Test]
        public void Test6543rdPrime()
        {
            Assert.AreEqual(65537, SimpleMath.GetPrimes().Skip(6542).Take(1).Single());
        }

        [Test]
        public void TestLargeInt32PrimeNumbers()
        {
            Assert.IsFalse(SimpleMath.GetPrimes().IsPrime(982451652));
            Assert.IsTrue(SimpleMath.GetPrimes().IsPrime(982451653));
            Assert.IsFalse(SimpleMath.GetPrimes().IsPrime(982451654));

            Assert.IsFalse(SimpleMath.GetPrimes().IsPrime(2147483628));
            Assert.IsTrue(SimpleMath.GetPrimes().IsPrime(2147483629));
            Assert.IsFalse(SimpleMath.GetPrimes().IsPrime(2147483630));
        }


        [Test]
        public void TestFactorization()
        {
            CollectionAssert.AreEqual(new[] { 2, 982451653 }, SimpleMath.GetPrimes().Factorize(2 * 982451653).ToList());

            CollectionAssert.AreEqual(new[] { 2, 3, 3, 5, 13 }, SimpleMath.GetPrimes().Factorize(2 * 3 * 3 * 5 * 13).ToList());

            CollectionAssert.AreEqual(new[] { 982451653 }, SimpleMath.GetPrimes().Factorize(982451653).ToList());

            CollectionAssert.AreEqual(new[] { 2, 2, 65537 }, SimpleMath.GetPrimes().Factorize(2 * 2 * 65537).ToList());
        }


        [Test]
        public void TestLastOfCachedPrimes()
        {
            Assert.IsFalse(SimpleMath.GetPrimes().IsPrime(65520));
            Assert.IsTrue(SimpleMath.GetPrimes().IsPrime(65521));
            Assert.IsFalse(SimpleMath.GetPrimes().IsPrime(65522));
        }

        [Test]
        public void ModRound()
        {
            Assert.AreEqual(8, SimpleMath.ModRound(10, 4));
            Assert.AreEqual(9, SimpleMath.ModRound(10, 3));
            Assert.AreEqual(100, SimpleMath.ModRound(106, 100));

            Assert.AreEqual(8L, SimpleMath.ModRound(10L, 4L));
            Assert.AreEqual(9L, SimpleMath.ModRound(10L, 3L));
            Assert.AreEqual(100L, SimpleMath.ModRound(106L, 100L));
        }

        [Test]
        public void NegativeModRound()
        {
            Assert.AreEqual(-12, SimpleMath.ModRound(-10, 4));
            Assert.AreEqual(-12, SimpleMath.ModRound(-10, 3));
            Assert.AreEqual(-200, SimpleMath.ModRound(-106, 100));

            Assert.AreEqual(-12L, SimpleMath.ModRound(-10L, 4L));
            Assert.AreEqual(-12L, SimpleMath.ModRound(-10L, 3L));
            Assert.AreEqual(-200L, SimpleMath.ModRound(-106L, 100L));
        }

        [Test]
        public void TestRealMod()
        {
            Assert.AreEqual(2, SimpleMath.RealMod(10, 4));
            Assert.AreEqual(2, SimpleMath.RealMod(-10, 4));

            Assert.AreEqual(1, SimpleMath.RealMod(10, 3));
            Assert.AreEqual(2, SimpleMath.RealMod(-10, 3));

            Assert.AreEqual(6, SimpleMath.RealMod(106, 100));
            Assert.AreEqual(94, SimpleMath.RealMod(-106, 100));
        }

        [Test]
        public void TestFloatEqTests()
        {
            Assert.IsTrue(SimpleMath.FloatEq(10.099m, 10m, 0.1m));
            Assert.IsFalse(SimpleMath.FloatEq(10.1m, 10m, 0.1m));

            Assert.IsTrue(SimpleMath.FloatEq(10.099, 10, 0.1));
        }
    }
}
