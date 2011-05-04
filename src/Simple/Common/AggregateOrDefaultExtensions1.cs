
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple
{
	public static partial class AggregateOrDefaultExtensions 
	{
		public static sbyte SumOrDefault(this IEnumerable<sbyte> source)
        {
            return source.SumOrDefault(default(sbyte));
        }

        public static sbyte SumOrDefault(this IEnumerable<sbyte> source, sbyte defaultValue)
        {
            var enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext()) return defaultValue;

            sbyte sum = enumerator.Current;
            while (enumerator.MoveNext())
                sum += enumerator.Current;
            return sum;
        }

        public static sbyte SumOrDefault(this IEnumerable<sbyte?> source)
        {
            return source.SumOrDefault(default(sbyte));
        }

        public static sbyte SumOrDefault(this IEnumerable<sbyte?> source, sbyte defaultValue)
        {
            return source.Where(x => x != null).Select(x => x ?? default(sbyte)).SumOrDefault(defaultValue);
        }
		public static byte SumOrDefault(this IEnumerable<byte> source)
        {
            return source.SumOrDefault(default(byte));
        }

        public static byte SumOrDefault(this IEnumerable<byte> source, byte defaultValue)
        {
            var enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext()) return defaultValue;

            byte sum = enumerator.Current;
            while (enumerator.MoveNext())
                sum += enumerator.Current;
            return sum;
        }

        public static byte SumOrDefault(this IEnumerable<byte?> source)
        {
            return source.SumOrDefault(default(byte));
        }

        public static byte SumOrDefault(this IEnumerable<byte?> source, byte defaultValue)
        {
            return source.Where(x => x != null).Select(x => x ?? default(byte)).SumOrDefault(defaultValue);
        }
		public static char SumOrDefault(this IEnumerable<char> source)
        {
            return source.SumOrDefault(default(char));
        }

        public static char SumOrDefault(this IEnumerable<char> source, char defaultValue)
        {
            var enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext()) return defaultValue;

            char sum = enumerator.Current;
            while (enumerator.MoveNext())
                sum += enumerator.Current;
            return sum;
        }

        public static char SumOrDefault(this IEnumerable<char?> source)
        {
            return source.SumOrDefault(default(char));
        }

        public static char SumOrDefault(this IEnumerable<char?> source, char defaultValue)
        {
            return source.Where(x => x != null).Select(x => x ?? default(char)).SumOrDefault(defaultValue);
        }
		public static short SumOrDefault(this IEnumerable<short> source)
        {
            return source.SumOrDefault(default(short));
        }

        public static short SumOrDefault(this IEnumerable<short> source, short defaultValue)
        {
            var enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext()) return defaultValue;

            short sum = enumerator.Current;
            while (enumerator.MoveNext())
                sum += enumerator.Current;
            return sum;
        }

        public static short SumOrDefault(this IEnumerable<short?> source)
        {
            return source.SumOrDefault(default(short));
        }

        public static short SumOrDefault(this IEnumerable<short?> source, short defaultValue)
        {
            return source.Where(x => x != null).Select(x => x ?? default(short)).SumOrDefault(defaultValue);
        }
		public static ushort SumOrDefault(this IEnumerable<ushort> source)
        {
            return source.SumOrDefault(default(ushort));
        }

        public static ushort SumOrDefault(this IEnumerable<ushort> source, ushort defaultValue)
        {
            var enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext()) return defaultValue;

            ushort sum = enumerator.Current;
            while (enumerator.MoveNext())
                sum += enumerator.Current;
            return sum;
        }

        public static ushort SumOrDefault(this IEnumerable<ushort?> source)
        {
            return source.SumOrDefault(default(ushort));
        }

        public static ushort SumOrDefault(this IEnumerable<ushort?> source, ushort defaultValue)
        {
            return source.Where(x => x != null).Select(x => x ?? default(ushort)).SumOrDefault(defaultValue);
        }
		public static int SumOrDefault(this IEnumerable<int> source)
        {
            return source.SumOrDefault(default(int));
        }

        public static int SumOrDefault(this IEnumerable<int> source, int defaultValue)
        {
            var enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext()) return defaultValue;

            int sum = enumerator.Current;
            while (enumerator.MoveNext())
                sum += enumerator.Current;
            return sum;
        }

        public static int SumOrDefault(this IEnumerable<int?> source)
        {
            return source.SumOrDefault(default(int));
        }

        public static int SumOrDefault(this IEnumerable<int?> source, int defaultValue)
        {
            return source.Where(x => x != null).Select(x => x ?? default(int)).SumOrDefault(defaultValue);
        }
		public static uint SumOrDefault(this IEnumerable<uint> source)
        {
            return source.SumOrDefault(default(uint));
        }

        public static uint SumOrDefault(this IEnumerable<uint> source, uint defaultValue)
        {
            var enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext()) return defaultValue;

            uint sum = enumerator.Current;
            while (enumerator.MoveNext())
                sum += enumerator.Current;
            return sum;
        }

        public static uint SumOrDefault(this IEnumerable<uint?> source)
        {
            return source.SumOrDefault(default(uint));
        }

        public static uint SumOrDefault(this IEnumerable<uint?> source, uint defaultValue)
        {
            return source.Where(x => x != null).Select(x => x ?? default(uint)).SumOrDefault(defaultValue);
        }
		public static long SumOrDefault(this IEnumerable<long> source)
        {
            return source.SumOrDefault(default(long));
        }

        public static long SumOrDefault(this IEnumerable<long> source, long defaultValue)
        {
            var enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext()) return defaultValue;

            long sum = enumerator.Current;
            while (enumerator.MoveNext())
                sum += enumerator.Current;
            return sum;
        }

        public static long SumOrDefault(this IEnumerable<long?> source)
        {
            return source.SumOrDefault(default(long));
        }

        public static long SumOrDefault(this IEnumerable<long?> source, long defaultValue)
        {
            return source.Where(x => x != null).Select(x => x ?? default(long)).SumOrDefault(defaultValue);
        }
		public static ulong SumOrDefault(this IEnumerable<ulong> source)
        {
            return source.SumOrDefault(default(ulong));
        }

        public static ulong SumOrDefault(this IEnumerable<ulong> source, ulong defaultValue)
        {
            var enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext()) return defaultValue;

            ulong sum = enumerator.Current;
            while (enumerator.MoveNext())
                sum += enumerator.Current;
            return sum;
        }

        public static ulong SumOrDefault(this IEnumerable<ulong?> source)
        {
            return source.SumOrDefault(default(ulong));
        }

        public static ulong SumOrDefault(this IEnumerable<ulong?> source, ulong defaultValue)
        {
            return source.Where(x => x != null).Select(x => x ?? default(ulong)).SumOrDefault(defaultValue);
        }
		public static float SumOrDefault(this IEnumerable<float> source)
        {
            return source.SumOrDefault(default(float));
        }

        public static float SumOrDefault(this IEnumerable<float> source, float defaultValue)
        {
            var enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext()) return defaultValue;

            float sum = enumerator.Current;
            while (enumerator.MoveNext())
                sum += enumerator.Current;
            return sum;
        }

        public static float SumOrDefault(this IEnumerable<float?> source)
        {
            return source.SumOrDefault(default(float));
        }

        public static float SumOrDefault(this IEnumerable<float?> source, float defaultValue)
        {
            return source.Where(x => x != null).Select(x => x ?? default(float)).SumOrDefault(defaultValue);
        }
		public static double SumOrDefault(this IEnumerable<double> source)
        {
            return source.SumOrDefault(default(double));
        }

        public static double SumOrDefault(this IEnumerable<double> source, double defaultValue)
        {
            var enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext()) return defaultValue;

            double sum = enumerator.Current;
            while (enumerator.MoveNext())
                sum += enumerator.Current;
            return sum;
        }

        public static double SumOrDefault(this IEnumerable<double?> source)
        {
            return source.SumOrDefault(default(double));
        }

        public static double SumOrDefault(this IEnumerable<double?> source, double defaultValue)
        {
            return source.Where(x => x != null).Select(x => x ?? default(double)).SumOrDefault(defaultValue);
        }
		public static decimal SumOrDefault(this IEnumerable<decimal> source)
        {
            return source.SumOrDefault(default(decimal));
        }

        public static decimal SumOrDefault(this IEnumerable<decimal> source, decimal defaultValue)
        {
            var enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext()) return defaultValue;

            decimal sum = enumerator.Current;
            while (enumerator.MoveNext())
                sum += enumerator.Current;
            return sum;
        }

        public static decimal SumOrDefault(this IEnumerable<decimal?> source)
        {
            return source.SumOrDefault(default(decimal));
        }

        public static decimal SumOrDefault(this IEnumerable<decimal?> source, decimal defaultValue)
        {
            return source.Where(x => x != null).Select(x => x ?? default(decimal)).SumOrDefault(defaultValue);
        }
		public static TSource MaxOrDefault<TSource>(this IEnumerable<TSource> source)
           where TSource : IComparable
        {
            return source.MaxOrDefault(default(TSource));
        }

        public static TResult MaxOrDefault<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> resultSelector)
            where TResult : IComparable
        {
            return source.Select(resultSelector).MaxOrDefault();
        }

        public static TResult MaxOrDefault<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> resultSelector, TResult defaultValue)
           where TResult : IComparable
        {
            return source.Select(resultSelector).MaxOrDefault(defaultValue);
        }

        public static TSource MaxOrDefault<TSource>(this IEnumerable<TSource?> source)
            where TSource : struct, IComparable
        {
            return source.MaxOrDefault(default(TSource));
        }

        public static TResult MaxOrDefault<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult?> resultSelector)
          where TResult : struct, IComparable
        {
            return source.Select(resultSelector).MaxOrDefault();
        }

        public static TSource MaxOrDefault<TSource>(this IEnumerable<TSource?> source, TSource defaultValue)
            where TSource : struct, IComparable
        {
            return source.Where(x => x != null).Select(x => x ?? default(TSource)).MaxOrDefault(defaultValue);
        }

        public static TResult MaxOrDefault<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult?> resultSelector, TResult defaultValue)
         where TResult : struct, IComparable
        {
            return source.Select(resultSelector).MaxOrDefault(defaultValue);
        }

		public static TSource MinOrDefault<TSource>(this IEnumerable<TSource> source)
           where TSource : IComparable
        {
            return source.MinOrDefault(default(TSource));
        }

        public static TResult MinOrDefault<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> resultSelector)
            where TResult : IComparable
        {
            return source.Select(resultSelector).MinOrDefault();
        }

        public static TResult MinOrDefault<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> resultSelector, TResult defaultValue)
           where TResult : IComparable
        {
            return source.Select(resultSelector).MinOrDefault(defaultValue);
        }

        public static TSource MinOrDefault<TSource>(this IEnumerable<TSource?> source)
            where TSource : struct, IComparable
        {
            return source.MinOrDefault(default(TSource));
        }

        public static TResult MinOrDefault<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult?> resultSelector)
          where TResult : struct, IComparable
        {
            return source.Select(resultSelector).MinOrDefault();
        }

        public static TSource MinOrDefault<TSource>(this IEnumerable<TSource?> source, TSource defaultValue)
            where TSource : struct, IComparable
        {
            return source.Where(x => x != null).Select(x => x ?? default(TSource)).MinOrDefault(defaultValue);
        }

        public static TResult MinOrDefault<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult?> resultSelector, TResult defaultValue)
         where TResult : struct, IComparable
        {
            return source.Select(resultSelector).MinOrDefault(defaultValue);
        }

	}
}