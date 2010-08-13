using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Globalization;

namespace Simple.Web.Mvc
{
    public class ElementalValueProvider : IValueProvider
    {
        public static class ValueProviderUtil
        {
            public static bool IsPrefixMatch(string prefix, string testString)
            {
                if (testString == null)
                {
                    return false;
                }

                if (prefix.Length == 0)
                {
                    return true; // shortcut - non-null testString matches empty prefix
                }

                if (!testString.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                {
                    return false; // prefix doesn't match
                }

                if (testString.Length == prefix.Length)
                {
                    return true; // exact match
                }

                // invariant: testString.Length > prefix.Length
                switch (testString[prefix.Length])
                {
                    case '.':
                    case '[':
                        return true; // known delimiters

                    default:
                        return false; // not known delimiter
                }
            }

        }

        public ElementalValueProvider(string name, object rawValue, CultureInfo culture)
        {
            Name = name;
            RawValue = rawValue;
            Culture = culture;
        }

        public CultureInfo Culture
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public object RawValue
        {
            get;
            private set;
        }

        public bool ContainsPrefix(string prefix)
        {
            return ValueProviderUtil.IsPrefixMatch(Name, prefix);
        }

        public ValueProviderResult GetValue(string key)
        {
            return (String.Equals(key, Name, StringComparison.OrdinalIgnoreCase))
                ? new ValueProviderResult(RawValue, Convert.ToString(RawValue, Culture), Culture)
                : null;
        }

    }
}
