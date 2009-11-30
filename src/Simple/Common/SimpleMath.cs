using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Simple.Patterns;

namespace Simple.Common
{
    public class SimpleMath
    {
        public static long RealMod(long p, long q)
        {
            long r = p % q;
            if (r < 0) return r + q;
            return r;
        }

        public static long ModRound(long p, long q)
        {
            return p - RealMod(p, q);
        }

        public static int ModRound(int p, int q)
        {
            return (int)(p - RealMod(p, q));
        }

        public static void Swap<T>(ref T a, ref T b)
        {
            T t = a;
            a = b;
            b = t;
        }

        public static bool FloatEq(decimal a, decimal b, decimal delta)
        {
            return Math.Abs(a - b) < delta;
        }

        public static bool FloatEq(float a, float b, float delta)
        {
            return Math.Abs(a - b) < delta;
        }

        public static bool FloatEq(double a, double b, double delta)
        {
            return Math.Abs(a - b) < delta;
        }

        public static PrimeNumbers GetPrimes()
        {
            return Singleton<PrimeNumbers>.Instance;
        }


    }
}
