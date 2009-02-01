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
        public static object Parse(string input, Type resultType)
        {
            object result = Activator.CreateInstance(resultType);

            int lastEnd = -1;

            CultureAttribute cultureAttr = ListExtensor.GetFirst<CultureAttribute>(
                resultType.GetCustomAttributes(typeof(CultureAttribute), true));

            CultureInfo defaultCulture = null;
            if (cultureAttr != null)
                defaultCulture = cultureAttr.Culture;
            else
                defaultCulture = CultureInfo.InvariantCulture;

            foreach (PropertyInfo prop in resultType.GetProperties())
            {
                StringOffsetAttribute attr = ListExtensor.GetFirst<StringOffsetAttribute>(
                    prop.GetCustomAttributes(typeof(StringOffsetAttribute), true));
                cultureAttr = ListExtensor.GetFirst<CultureAttribute>(
                    prop.GetCustomAttributes(typeof(CultureAttribute), true));

                CultureInfo currCulture = null;
                if (cultureAttr != null)
                    currCulture = cultureAttr.Culture;
                else
                    currCulture = defaultCulture;



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
                    Convert.ChangeType(input.Substring(start, length), prop.PropertyType, currCulture),
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
            where T:new()
        {
            StreamReader reader = new StreamReader(input);
            return Parse<T>(reader);
        }

        public static IList<T> Parse<T>(StreamReader input)
            where T:new()
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
            where T:new()
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
