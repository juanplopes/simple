using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace BasicLibrary.Common
{
    public class PrimeNumbers
    {
        protected static BitArray Numbers { get; set; }
        protected static IList<int> Primes { get; set; }

        public const int TopSieveNumber = 1<<14;
        public static int PrimeCount
        {
            get
            {
                EnsureInitialized();
                return Primes.Count;
            }
        }

        protected static void EnsureInitialized()
        {
            if (Numbers == null)
            {
                Numbers = new BitArray(TopSieveNumber, true);
                for (int i = 2; i < TopSieveNumber; i++)
                    if (Numbers[i])
                        for (int j = i * 2; j < TopSieveNumber; j += i)
                            Numbers[j] = false;

                Primes = new List<int>();
                for (int i = 1; i < TopSieveNumber; i++)
                    if (Numbers[i])
                        Primes.Add(i);
            }
        }

        public static IEnumerable<int> GetPrimeEnumrable()
        {
            EnsureInitialized();
            foreach (int i in Primes)
            {
                yield return i;
            }
        }

        public static int GetByIndex(int idx)
        {
            EnsureInitialized();
            return Primes[idx];
        }

    }
}
