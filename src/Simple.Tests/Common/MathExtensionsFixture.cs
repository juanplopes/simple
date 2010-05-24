using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Simple.Common;

namespace Simple.Tests.Common
{
    [TestFixture]
    public class MathExtensionsFixture
    {
        [Test]
        public void FirstPrimes()
        {
            List<int> primes = new List<int>(MathExtensions.GetPrimes().Limit(12));
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
            Assert.AreEqual(6542, MathExtensions.GetPrimes().CachedCount);
        }

        [Test]
        public void TestNoNegativePrimes()
        {
            Assert.IsFalse(MathExtensions.GetPrimes().IsPrime(-11));
            Assert.IsFalse(MathExtensions.GetPrimes().IsPrime(-9));
        }

        [Test]
        public void TestPrimeCacheLimits()
        {
            Assert.IsFalse(MathExtensions.GetPrimes().IsPrime(65520));
            Assert.IsTrue(MathExtensions.GetPrimes().IsPrime(65521));
            Assert.IsFalse(MathExtensions.GetPrimes().IsPrime(65522));
        }

        [Test]
        public void Test6543rdPrime()
        {
            Assert.AreEqual(65537, MathExtensions.GetPrimes().Skip(6542).Take(1).Single());
        }

        [Test]
        public void TestLargeInt32PrimeNumbers()
        {
            Assert.IsFalse(MathExtensions.GetPrimes().IsPrime(982451652));
            Assert.IsTrue(MathExtensions.GetPrimes().IsPrime(982451653));
            Assert.IsFalse(MathExtensions.GetPrimes().IsPrime(982451654));

            Assert.IsFalse(MathExtensions.GetPrimes().IsPrime(2147483628));
            Assert.IsTrue(MathExtensions.GetPrimes().IsPrime(2147483629));
            Assert.IsFalse(MathExtensions.GetPrimes().IsPrime(2147483630));
        }


        [Test]
        public void TestFactorization()
        {
            CollectionAssert.AreEqual(new[] { 2, 982451653 }, MathExtensions.GetPrimes().Factorize(2 * 982451653).ToList());

            CollectionAssert.AreEqual(new[] { 2, 3, 3, 5, 13 }, MathExtensions.GetPrimes().Factorize(2 * 3 * 3 * 5 * 13).ToList());

            CollectionAssert.AreEqual(new[] { 982451653 }, MathExtensions.GetPrimes().Factorize(982451653).ToList());

            CollectionAssert.AreEqual(new[] { 2, 2, 65537 }, MathExtensions.GetPrimes().Factorize(2 * 2 * 65537).ToList());
        }


        [Test]
        public void TestLastOfCachedPrimes()
        {
            Assert.IsFalse(MathExtensions.GetPrimes().IsPrime(65520));
            Assert.IsTrue(MathExtensions.GetPrimes().IsPrime(65521));
            Assert.IsFalse(MathExtensions.GetPrimes().IsPrime(65522));
        }

        [Test]
        public void ModRound()
        {
            Assert.AreEqual(8, MathExtensions.ModRound(10, 4));
            Assert.AreEqual(9, MathExtensions.ModRound(10, 3));
            Assert.AreEqual(100, MathExtensions.ModRound(106, 100));

            Assert.AreEqual(8L, MathExtensions.ModRound(10L, 4L));
            Assert.AreEqual(9L, MathExtensions.ModRound(10L, 3L));
            Assert.AreEqual(100L, MathExtensions.ModRound(106L, 100L));
        }

        [Test]
        public void NegativeModRound()
        {
            Assert.AreEqual(-12, MathExtensions.ModRound(-10, 4));
            Assert.AreEqual(-12, MathExtensions.ModRound(-10, 3));
            Assert.AreEqual(-200, MathExtensions.ModRound(-106, 100));

            Assert.AreEqual(-12L, MathExtensions.ModRound(-10L, 4L));
            Assert.AreEqual(-12L, MathExtensions.ModRound(-10L, 3L));
            Assert.AreEqual(-200L, MathExtensions.ModRound(-106L, 100L));
        }

        [Test]
        public void TestRealMod()
        {
            Assert.AreEqual(2, MathExtensions.RealMod(10, 4));
            Assert.AreEqual(2, MathExtensions.RealMod(-10, 4));

            Assert.AreEqual(1, MathExtensions.RealMod(10, 3));
            Assert.AreEqual(2, MathExtensions.RealMod(-10, 3));

            Assert.AreEqual(6, MathExtensions.RealMod(106, 100));
            Assert.AreEqual(94, MathExtensions.RealMod(-106, 100));
        }

        [Test]
        public void TestFloatEqTests()
        {
            Assert.IsTrue(MathExtensions.FloatEq(10.099m, 10m, 0.1m));
            Assert.IsFalse(MathExtensions.FloatEq(10.1m, 10m, 0.1m));

            Assert.IsTrue(MathExtensions.FloatEq(10.099, 10, 0.1));
        }
    }
}
