using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Simple.Generator.Strings
{
    public static class GeneratorStrings
    {
        public static string ToTitleCase(this string word)
        {
            return Regex.Replace(ToHumanCase(AddUnderscores(word)), @"\b([a-z])",
                delegate(Match match) { return match.Captures[0].Value.ToUpper(); });
        }

        public static string ToHumanCase(this string lowercaseAndUnderscoredWord)
        {
            return MakeInitialCaps(Regex.Replace(lowercaseAndUnderscoredWord, @"_", " "));
        }


        public static string AddUnderscores(this string pascalCasedWord)
        {
            return Regex.Replace(Regex.Replace(Regex.Replace(pascalCasedWord, @"([A-Z]+)([A-Z][a-z])", "$1_$2"), @"([a-z\d])([A-Z])", "$1_$2"), @"[-\s]", "_").ToLower();
        }

        public static string MakeInitialCaps(this string word)
        {
            return String.Concat(word.Substring(0, 1).ToUpper(), word.Substring(1).ToLower());
        }

        public static string MakeInitialLowerCase(this string word)
        {
            return String.Concat(word.Substring(0, 1).ToLower(), word.Substring(1));
        }


        public static bool IsStringNumeric(this string str)
        {
            double result;
            return (double.TryParse(str, NumberStyles.Float, NumberFormatInfo.CurrentInfo, out result));
        }

        public static string AddOrdinalSuffix(this string number)
        {
            if (IsStringNumeric(number))
            {
                int n = int.Parse(number);
                int nMod100 = n % 100;

                if (nMod100 >= 11 && nMod100 <= 13)
                    return String.Concat(number, "th");

                switch (n % 10)
                {
                    case 1:
                        return String.Concat(number, "st");
                    case 2:
                        return String.Concat(number, "nd");
                    case 3:
                        return String.Concat(number, "rd");
                    default:
                        return String.Concat(number, "th");
                }
            }
            return number;
        }

        public static string ConvertUnderscoresToDashes(this string underscoredWord)
        {
            return underscoredWord.Replace('_', '-');
        }

        public static string ToPlural(this string word, IPluralizer pluralizer)
        {
            return pluralizer.ToPlural(word);
        }

        public static string ToSingular(this string word, IPluralizer pluralizer)
        {
            return pluralizer.ToSingular(word);
        }

        public static string CleanUp(this string propertyToFilter)
        {
            string propertyName = string.Empty;
            string[] nameParts = propertyToFilter.Replace(' ', '_').Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string namePart in nameParts)
            {
                string temp = namePart;
                if (namePart.ToUpper() == namePart) temp = namePart.ToLower();
                propertyName += temp.Substring(0, 1).ToUpper() + temp.Substring(1);
            }

            if (string.IsNullOrEmpty(propertyName))
                throw new Exception("Cannot fix the property name!");

            return propertyName;
        }

        public static string ReplaceId(this string column)
        {
            if (column.StartsWith("Id", StringComparison.InvariantCultureIgnoreCase))
                if (column.Length > 2 && !(column[2] >= 'a' && column[2] <= 'z'))
                    return column.Substring(2);

            if (column.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
                return column.Remove(column.Length - 2);

            return column;
        }
    }
}
