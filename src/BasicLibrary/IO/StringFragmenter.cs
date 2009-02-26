using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using BasicLibrary.Common;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Collections;

namespace BasicLibrary.IO
{
    public class StringFragmenter
    {
        private static CultureInfo GetCulture(MemberInfo member, CultureInfo defaultOne)
        {
            CultureAttribute cultureAttr = Enumerable.GetFirst<CultureAttribute>(
                member.GetCustomAttributes(typeof(CultureAttribute), true));
            if (cultureAttr != null)
                return cultureAttr.Culture;
            else
                return defaultOne;

        }

        private static IFormatter GetParser(MemberInfo member, IFormatter defaultOne)
        {
            FormatterAttribute parserAttr = Enumerable.GetFirst<FormatterAttribute>(
                member.GetCustomAttributes(typeof(FormatterAttribute), true));
            if (parserAttr != null)
                return parserAttr.Instance;
            else
                return defaultOne;
        }

        public static string Write(object input)
        {
            StringBuilder builder = new StringBuilder();

            if (input == null) throw new NullReferenceException();

            CultureInfo defaultCulture = GetCulture(input.GetType(), CultureInfo.InvariantCulture);
            IFormatter defaultParser = GetParser(input.GetType(), DefaultFormatter.Instance);

            int lastEnd = -1;
            int maxSize = 0;
            foreach (PropertyInfo prop in input.GetType().GetProperties())
            {
                StringOffsetAttribute attr = Enumerable.GetFirst<StringOffsetAttribute>(
                    prop.GetCustomAttributes(typeof(StringOffsetAttribute), true));

                if (attr == null) continue;

                CultureInfo currCulture = GetCulture(prop, defaultCulture);
                IFormatter currParser = GetParser(prop, defaultParser);

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

                lastEnd = start + length - 1;

                maxSize = Math.Max(maxSize, start + length);
                builder.Length = maxSize;

                string formatted = currParser.Format(prop.GetValue(input, null), currCulture);
                builder.Insert(start,
                    formatted.Substring(0, Math.Min(length, formatted.Length)));
            }

            return builder.ToString().Substring(0, maxSize);
        }

        public static object Parse(string input, Type resultType)
        {
            object result = Activator.CreateInstance(resultType);

            CultureInfo defaultCulture = GetCulture(resultType, CultureInfo.InvariantCulture);
            IFormatter defaultParser = GetParser(resultType, DefaultFormatter.Instance);

            int lastEnd = -1;
            foreach (PropertyInfo prop in resultType.GetProperties())
            {
                StringOffsetAttribute attr = Enumerable.GetFirst<StringOffsetAttribute>(
                    prop.GetCustomAttributes(typeof(StringOffsetAttribute), true));

                if (attr == null) continue;

                CultureInfo currCulture = GetCulture(prop, defaultCulture);
                IFormatter currParser = GetParser(prop, defaultParser);

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

                lastEnd = start + length - 1;

                prop.SetValue(result,
                    currParser.Parse(input.Substring(start, length), prop.PropertyType, currCulture),
                    null);
            }

            if (result is ICorrectible)
                (result as ICorrectible).CorrectOnLoad();

            return result;
        }

        public static T Parse<T>(string input)
            where T : new()
        {
            return (T)Parse(input, typeof(T));
        }

        public static IEnumerable<T> Parse<T>(Stream input)
            where T : new()
        {
            StreamReader reader = new StreamReader(input);
            return Parse<T>(reader);
        }

        public static void Write(Stream output, object input)
        {
            Write(new StreamWriter(output), input);
        }

        public static void WriteEnum(Stream output, IEnumerable input)
        {
            WriteEnum(new StreamWriter(output), input);
        }

        public static void Write(StreamWriter output, object input)
        {
            string temp = Write(input);
            output.WriteLine(temp);
            output.Flush();
        }

        public static void WriteEnum(StreamWriter output, IEnumerable input)
        {
            foreach (object singInput in input)
            {
                Write(output, singInput);
            }
        }


        private static IEnumerable<T> ParseInternal<T>(StreamReader input)
            where T : new()
        {
            while (!input.EndOfStream)
            {
                string value = input.ReadLine();
                if (value != null && !string.IsNullOrEmpty(value.Trim()))
                    yield return Parse<T>(value);
            }
        }

        public static IEnumerable<T> Parse<T>(StreamReader input)
            where T : new()
        {
            return Enumerable.ToLazy(ParseInternal<T>(input));
        }

        public static IEnumerable<T> Parse<T>(string[] input)
            where T : new()
        {
            foreach (string s in input)
            {
                yield return Parse<T>(s);
            }
        }
    }
}
