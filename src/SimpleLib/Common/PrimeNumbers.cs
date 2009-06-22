using System;
using System.Collections.Generic;
using System.Collections;
using Simple.Patterns;

namespace Simple.Common
{
    public class PrimeNumbers
    {
        public const int TopSieveNumber = 0x10001;
        public static IEnumerable<int> GetPrimesEnumerable(int topSieveNumber)
        {
            BitArray sieve = new BitArray(topSieveNumber, true);
            sieve[0] = false; sieve[1] = false;

            int sqrTop = Convert.ToInt32(Math.Ceiling(Math.Sqrt(topSieveNumber))) + 1;

            for (int i = 2; i < topSieveNumber; i++)
                if (sieve[i])
                {
                    yield return i;
                    for (int j = i * 2; j < sqrTop; j += i)
                        sieve[j] = false;
                }
        }

        protected static IEnumerable<int> _primes =
            new LazyEnumerable<int>(GetPrimesEnumerable(TopSieveNumber)).Activate();

        public static IEnumerable<int> GetPrimesEnumrable()
        {
            return _primes;
        }
    }
}
