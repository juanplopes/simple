using System;
using Simple.Patterns;

namespace Simple.Common
{
    /// <summary>
    /// Contains helper methods for working with simple math operations.
    /// </summary>
    public class SimpleMath
    {
        /// <summary>
        /// Apply the real modular operation, returning values in range [0, q).
        /// </summary>
        /// <param name="p">The dividend.</param>
        /// <param name="q">The divisor.</param>
        /// <returns>The modulo q result.</returns>
        public static long RealMod(long p, long q)
        {
            long r = p % q;
            if (r < 0) return r + q;
            return r;
        }


        /// <summary>
        /// Rounds an integer number to the first less than or equal number that is 0 modulo q.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <returns>The rounded number.</returns>
        public static long ModRound(long p, long q)
        {
            return p - RealMod(p, q);
        }
        /// <summary>
        /// Rounds an integer number to the first less than or equal number that is 0 modulo q.
        /// </summary>
        /// <param name="p">The number.</param>
        /// <param name="q">The modular set.</param>
        /// <returns>The rounded number.</returns>
        public static int ModRound(int p, int q)
        {
            return (int)(p - RealMod(p, q));
        }

        /// <summary>
        /// Swaps two variables.
        /// </summary>
        /// <typeparam name="T">The variables type.</typeparam>
        /// <param name="a">First value.</param>
        /// <param name="b">Second value.</param>
        public static void Swap<T>(ref T a, ref T b)
        {
            T t = a;
            a = b;
            b = t;
        }

        /// <summary>
        /// Compares two decimal values for equality inside some delta.
        /// </summary>
        /// <param name="a">First value.</param>
        /// <param name="b">Second value.</param>
        /// <param name="delta">The delta value.</param>
        /// <returns>True if they are almost equal, false otherwise.</returns>
        public static bool FloatEq(decimal a, decimal b, decimal delta)
        {
            return Math.Abs(a - b) < delta;
        }

        /// <summary>
        /// Compares two float values for equality inside some delta.
        /// </summary>
        /// <param name="a">First value.</param>
        /// <param name="b">Second value.</param>
        /// <param name="delta">The delta value.</param>
        /// <returns>True if they are almost equal, false otherwise.</returns>
        public static bool FloatEq(float a, float b, float delta)
        {
            return Math.Abs(a - b) < delta;
        }

        /// <summary>
        /// Compares two double values for equality inside some delta.
        /// </summary>
        /// <param name="a">First value.</param>
        /// <param name="b">Second value.</param>
        /// <param name="delta">The delta value.</param>
        /// <returns>True if they are almost equal, false otherwise.</returns>
        public static bool FloatEq(double a, double b, double delta)
        {
            return Math.Abs(a - b) < delta;
        }

        /// <summary>
        /// Returns the singleton instance for the prime numbers enumerable.
        /// </summary>
        /// <returns>The enumerable.</returns>
        public static PrimeNumbers GetPrimes()
        {
            return Singleton<PrimeNumbers>.Instance;
        }


    }
}
