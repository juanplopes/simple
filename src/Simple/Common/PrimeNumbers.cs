using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Simple.Common
{
    /// <summary>
    /// Manages a prime numbers list using a pre-generated Sieve of Eratosthenes.
    /// </summary>
    public class PrimeNumbers : IEnumerable<int>
    {
        private BitArray _sieve;
        private IList<int> _cache;

        /// <summary>
        /// Constructs using a sieve calculated up to 65,636.
        /// </summary>
        public PrimeNumbers() : this(Resources.Primes) { }

        /// <summary>
        /// Constructs using any byte array instance.
        /// </summary>
        /// <param name="sieve">The construction data. Every byte must hold exactly 8 number states.</param>
        public PrimeNumbers(byte[] sieve)
            : this(new BitArray(sieve))
        {
        }

        /// <summary>
        /// Constructs using a BitArray instance
        /// </summary>
        /// <param name="sieve">The sieve data, formatted as a BitArray.</param>
        public PrimeNumbers(BitArray sieve)
        {
            _sieve = sieve;
            _cache = new List<int>(EnumerateSieve(sieve));
        }

        /// <summary>
        /// Enumerates through all primes up to <paramref name="maxPrime"/>.
        /// </summary>
        /// <param name="maxPrime">The maximum value (exclusive) to test.</param>
        /// <returns>A lazy enumerable of primes.</returns>
        public IEnumerable<int> Limit(int maxPrime)
        {
            return this.TakeWhile(x => x < maxPrime);
        }

        /// <summary>
        /// Gets the number of itens checked by the sieve, passed to the class constructor.
        /// </summary>
        public int CachedCount { get { return _cache.Count; } }

        /// <summary>
        /// Private method for enumerate all the primes inside a sieve.
        /// </summary>
        /// <param name="sieve">The sieve in a BitArray format.</param>
        /// <returns>A lazy enumerable of primes.</returns>
        private static IEnumerable<int> EnumerateSieve(BitArray sieve)
        {
            for (int i = 0; i < sieve.Length; i++)
                if (sieve[i]) yield return i;
        }

        /// <summary>
        /// Splits a complex positive integer number into its prime factors.
        /// </summary>
        /// <param name="number">The number to be factorized.</param>
        /// <returns>The factors enumerated (in a increasing order).</returns>
        public IEnumerable<int> Factorize(int number)
        {
            int d, r;
            foreach (int i in this.Limit((int)Math.Floor(Math.Sqrt(number))))
            {
                if (number == 1) yield break;

                while (true)
                {
                    d = Math.DivRem(number, i, out r);
                    if (r != 0) break;
                    
                    //r == 0
                    yield return i;
                    number = d;
                }

            }

            yield return number;
        }

        /// <summary>
        /// Checks for a number primality.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns>True if it is prime. False otherwise.</returns>
        public bool IsPrime(int number)
        {
            if (number < 0) return false;

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

        /// <summary>
        /// Returns the type enumerator representing virtually all integer prime numbers.
        /// </summary>
        /// <returns>The primes enumerator.</returns>
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
