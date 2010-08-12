using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SharpTestsEx;
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
            primes.Count.Should().Be(5);

            primes[0].Should().Be(2);
            primes[1].Should().Be(3);
            primes[2].Should().Be(5);
            primes[3].Should().Be(7);
            primes[4].Should().Be(11);
        }

        [Test]
        public void TestPrimeCacheInfo()
        {
            MathExtensions.GetPrimes().CachedCount.Should().Be(6542);
        }

        [Test]
        public void TestNoNegativePrimes()
        {
            MathExtensions.GetPrimes().IsPrime(-11).Should().Be.False();
            MathExtensions.GetPrimes().IsPrime(-9).Should().Be.False();
        }

        [Test]
        public void TestPrimeCacheLimits()
        {
            MathExtensions.GetPrimes().IsPrime(65520).Should().Be.False();
            MathExtensions.GetPrimes().IsPrime(65521).Should().Be.True();
            MathExtensions.GetPrimes().IsPrime(65522).Should().Be.False();
        }

        [Test]
        public void Test6543rdPrime()
        {
            MathExtensions.GetPrimes().Skip(6542).Take(1).Single().Should().Be(65537);
        }

        [Test]
        public void TestLargeInt32PrimeNumbers()
        {
            MathExtensions.GetPrimes().IsPrime(982451652).Should().Be.False();
            MathExtensions.GetPrimes().IsPrime(982451653).Should().Be.True();
            MathExtensions.GetPrimes().IsPrime(982451654).Should().Be.False();

            MathExtensions.GetPrimes().IsPrime(2147483628).Should().Be.False();
            MathExtensions.GetPrimes().IsPrime(2147483629).Should().Be.True();
            MathExtensions.GetPrimes().IsPrime(2147483630).Should().Be.False();
        }


        [Test]
        public void TestFactorization()
        {
            var primes = MathExtensions.GetPrimes();

            primes.Factorize(2 * 982451653)
                .Should().Have.SameSequenceAs(2, 982451653);

            primes.Factorize(2 * 3 * 3 * 5 * 13)
                .Should().Have.SameSequenceAs(2, 3, 3, 5, 13);

            primes.Factorize(982451653)
                .Should().Have.SameSequenceAs(new[] { 982451653 });

            primes.Factorize(2 * 2 * 65537)
                .Should().Have.SameSequenceAs(new[] { 2, 2, 65537 });
        }


        [Test]
        public void TestLastOfCachedPrimes()
        {
            var primes = MathExtensions.GetPrimes();
            primes.IsPrime(65520).Should().Be.False();
            primes.IsPrime(65521).Should().Be.True();
            primes.IsPrime(65522).Should().Be.False();
        }

        [Test]
        public void ModRound()
        {
            MathExtensions.ModRound(10, 4).Should().Be(8);
            MathExtensions.ModRound(10, 3).Should().Be(9);
            MathExtensions.ModRound(106, 100).Should().Be(100);

            MathExtensions.ModRound(10L, 4L).Should().Be(8L);
            MathExtensions.ModRound(10L, 3L).Should().Be(9L);
            MathExtensions.ModRound(106L, 100L).Should().Be(100L);
        }

        [Test]
        public void NegativeModRound()
        {
            MathExtensions.ModRound(-10, 4).Should().Be(-12);
            MathExtensions.ModRound(-10, 3).Should().Be(-12);
            MathExtensions.ModRound(-106, 100).Should().Be(-200);

            MathExtensions.ModRound(-10L, 4L).Should().Be(-12L);
            MathExtensions.ModRound(-10L, 3L).Should().Be(-12L);
            MathExtensions.ModRound(-106L, 100L).Should().Be(-200L);
        }

        [Test]
        public void TestRealMod()
        {
            MathExtensions.RealMod(10, 4).Should().Be(2);
            MathExtensions.RealMod(-10, 4).Should().Be(2);

            MathExtensions.RealMod(10, 3).Should().Be(1);
            MathExtensions.RealMod(-10, 3).Should().Be(2);

            MathExtensions.RealMod(106, 100).Should().Be(6);
            MathExtensions.RealMod(-106, 100).Should().Be(94);
        }

        [Test]
        public void TestFloatEqTests()
        {
            MathExtensions.FloatEq(10.099m, 10m, 0.1m).Should().Be.True();
            MathExtensions.FloatEq(10.1m, 10m, 0.1m).Should().Be.False();
            MathExtensions.FloatEq(10.099, 10, 0.1).Should().Be.True();
        }
    }
}
