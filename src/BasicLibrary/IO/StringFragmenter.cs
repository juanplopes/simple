using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using BasicLibrary.Common;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace BasicLibrary.IO
{
    public class StringFragmenter
    {
        private static CultureInfo GetCulture(MemberInfo member, CultureInfo defaultOne)
        {
            CultureAttribute cultureAttr = ListExtensor.GetFirst<CultureAttribute>(
                member.GetCustomAttributes(typeof(CultureAttribute), true));
            if (cultureAttr != null)
                return cultureAttr.Culture;
            else
                return defaultOne;

        }

        private static IParser GetParser(MemberInfo member, IParser defaultOne)
        {
            ParserAttribute parserAttr = ListExtensor.GetFirst<ParserAttribute>(
                member.GetCustomAttributes(typeof(ParserAttribute), true));
            if (parserAttr != null)
                return parserAttr.CreateInstance();
            else
                return defaultOne;
        }

        public static object Parse(string input, Type resultType)
        {
            object result = Activator.CreateInstance(resultType);

            CultureInfo defaultCulture = GetCulture(resultType, CultureInfo.InvariantCulture);
            IParser defaultParser = GetParser(resultType, DefaultParser.Instance);

            int lastEnd = -1;
            foreach (PropertyInfo prop in resultType.GetProperties())
            {
                StringOffsetAttribute attr = ListExtensor.GetFirst<StringOffsetAttribute>(
                    prop.GetCustomAttributes(typeof(StringOffsetAttribute), true));

                CultureInfo currCulture = GetCulture(prop, defaultCulture);
                IParser currParser = GetParser(prop, defaultParser);

                int start, length;
                if (attr.Start != null)
                {
                    start = attr.Start ?? 0;
                    length = attr.Length;
                }
                else
                {
                    start = lastEnd + 1;
                    length = attr.Length;
                }

                Debug.Assert(start + length - 1 < input.Length, "End must be lesser than string length");
                Debug.Assert(start >= 0, "Start must be non-negative");
                lastEnd = start + length - 1;

                prop.SetValue(result,
                    currParser.Parse(input.Substring(start, length), prop.PropertyType, currCulture),
                    null);
            }

            if (result is ICorrectible)
                (result as ICorrectible).CorrectMe();

            return result;
        }

        public static T Parse<T>(string input)
            where T : new()
        {
            return (T)Parse(input, typeof(T));
        }

        public static IList<T> Parse<T>(Stream input)
            where T : new()
        {
            StreamReader reader = new StreamReader(input);
            return Parse<T>(reader);
        }

        public static IList<T> Parse<T>(StreamReader input)
            where T : new()
        {
            IList<T> result = new List<T>();
            while (!input.EndOfStream)
            {
                string value = input.ReadLine();
                result.Add(Parse<T>(value));
            }
            return result;
        }

        public static IList<T> Parse<T>(string[] input)
            where T : new()
        {
            IList<T> result = new List<T>();
            foreach (string s in input)
            {
                result.Add(Parse<T>(s));
            }
            return result;
        }
    }
}
