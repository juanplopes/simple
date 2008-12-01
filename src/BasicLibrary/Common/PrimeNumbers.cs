using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace BasicLibrary.Common
{
    public class PrimeNumbers
    {
        public IEnumerable<int> GetPrimeEnumerable(int topNumber)
        {
            BitArray numbers = new BitArray(topNumber, true);

            for (int i = 2; i < topNumber; i++)
                if (numbers[i])
                    for (int j = i * 2; j < topNumber; j += i)
                        numbers[j] = false;


            for (int i = 1; i < topNumber; i++)
                if (numbers[i])
                    yield return i;
        }


    }
}
