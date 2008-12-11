using System;
using System.Collections.Generic;
using System.Text;

namespace BasicLibrary.Common
{
    public class MathHelper
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
            return (int)ModRound(p, q);
        }

        public static void Swap<T>(ref T a, ref T b)
        {
            T t = a;
            a = b;
            b = t;
        }
    }
}
