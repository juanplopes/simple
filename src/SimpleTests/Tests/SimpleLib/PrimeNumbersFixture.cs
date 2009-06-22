using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simple.Common;

namespace Simple.Tests.SimpleLib
{
    [TestClass]
    public class PrimeNumbersFixture
    {
        [TestMethod]
        public void FirstPrimes()
        {
            List<int> primes = new List<int>(PrimeNumbers.GetPrimesEnumrable());
            Assert.AreEqual(2, primes[0]);
            Assert.AreEqual(3, primes[1]);
            Assert.AreEqual(5, primes[2]);
            Assert.AreEqual(7, primes[3]);
            Assert.AreEqual(11, primes[4]);
        }
    }
}
