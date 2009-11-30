using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Simple.Patterns;

namespace Simple.Common
{
    public class PrimeNumbers : IEnumerable<int>
    {
        private BitArray _sieve;
        private IList<int> _cache;

        public PrimeNumbers() : this(Resources.Primes) { }

        public PrimeNumbers(byte[] sieve)
            : this(new BitArray(sieve))
        {
        }

        public PrimeNumbers(BitArray sieve)
        {
            _sieve = sieve;
            _cache = new List<int>(EnumerateSieve(sieve));
        }

        public IEnumerable<int> Limit(int maxPrime)
        {
            foreach (int i in this)
            {
                if (i > maxPrime) break;
                yield return i;
            }
        }

        public int CachedCount { get { return _cache.Count; } }

        private static IEnumerable<int> EnumerateSieve(BitArray sieve)
        {
            for (int i = 0; i < sieve.Length; i++)
                if (sieve[i]) yield return i;
        }

        public bool IsPrime(int number)
        {
            if (number < 0) number = -number;

            if (number < _sieve.Length)
                return _sieve[number];
            else
            {
                foreach (int i in this.Limit((int)Math.Floor(Math.Sqrt(number))))
                    if (number % i == 0) return false;

                return true;
            }
        }

        #region IEnumerable<int> Members

        public IEnumerator<int> GetEnumerator()
        {
            foreach (int i in _cache)
                yield return i;

            for (int i = _sieve.Length; i < int.MaxValue; i++)
                if (IsPrime(i)) yield return i;
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}
