using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Sample.Project.Generator
{
    public static class Pluralizer
    {
        private static readonly List<InflectorRule> _plurals = new List<InflectorRule>();
        private static readonly List<InflectorRule> _singulars = new List<InflectorRule>();
        private static readonly List<string> _uncountables = new List<string>();

        static Pluralizer()
        {
            AddPluralRule("$", "s");
            AddPluralRule("s$", "s");
            AddPluralRule("(ax|test)is$", "$1es");
            AddPluralRule("(octop|vir)us$", "$1i");
            AddPluralRule("(alias|status)$", "$1es");
            AddPluralRule("(bu)s$", "$1ses");
            AddPluralRule("(buffal|tomat)o$", "$1oes");
            AddPluralRule("([ti])um$", "$1a");
            AddPluralRule("sis$", "ses");
            AddPluralRule("(?:([^f])fe|([lr])f)$", "$1$2ves");
            AddPluralRule("(hive)$", "$1s");
            AddPluralRule("([^aeiouy]|qu)y$", "$1ies");
            AddPluralRule("(x|ch|ss|sh)$", "$1es");
            AddPluralRule("(matr|vert|ind)ix|ex$", "$1ices");
            AddPluralRule("([m|l])ouse$", "$1ice");
            AddPluralRule("^(ox)$", "$1en");
            AddPluralRule("(quiz)$", "$1zes");

            AddSingularRule("s$", String.Empty);
            AddSingularRule("ss$", "ss");
            AddSingularRule("(n)ews$", "$1ews");
            AddSingularRule("([ti])a$", "$1um");
            AddSingularRule("((a)naly|(b)a|(d)iagno|(p)arenthe|(p)rogno|(s)ynop|(t)he)ses$", "$1$2sis");
            AddSingularRule("(^analy)ses$", "$1sis");
            AddSingularRule("([^f])ves$", "$1fe");
            AddSingularRule("(hive)s$", "$1");
            AddSingularRule("(tive)s$", "$1");
            AddSingularRule("([lr])ves$", "$1f");
            AddSingularRule("([^aeiouy]|qu)ies$", "$1y");
            AddSingularRule("(s)eries$", "$1eries");
            AddSingularRule("(m)ovies$", "$1ovie");
            AddSingularRule("(x|ch|ss|sh)es$", "$1");
            AddSingularRule("([m|l])ice$", "$1ouse");
            AddSingularRule("(bus)es$", "$1");
            AddSingularRule("(o)es$", "$1");
            AddSingularRule("(shoe)s$", "$1");
            AddSingularRule("(cris|ax|test)es$", "$1is");
            AddSingularRule("(octop|vir)i$", "$1us");
            AddSingularRule("(alias|status)$", "$1");
            AddSingularRule("(alias|status)es$", "$1");
            AddSingularRule("^(ox)en", "$1");
            AddSingularRule("(vert|ind)ices$", "$1ex");
            AddSingularRule("(matr)ices$", "$1ix");
            AddSingularRule("(quiz)zes$", "$1");

            AddIrregularRule("person", "people");
            AddIrregularRule("man", "men");
            AddIrregularRule("child", "children");
            AddIrregularRule("sex", "sexes");
            AddIrregularRule("tax", "taxes");
            AddIrregularRule("move", "moves");

            AddUnknownCountRule("equipment");
            AddUnknownCountRule("information");
            AddUnknownCountRule("rice");
            AddUnknownCountRule("money");
            AddUnknownCountRule("species");
            AddUnknownCountRule("series");
            AddUnknownCountRule("fish");
            AddUnknownCountRule("sheep");
        }

        private static void AddIrregularRule(string singular, string plural)
        {
            AddPluralRule(String.Concat("(", singular[0], ")", singular.Substring(1), "$"), String.Concat("$1", plural.Substring(1)));
            AddSingularRule(String.Concat("(", plural[0], ")", plural.Substring(1), "$"), String.Concat("$1", singular.Substring(1)));
        }

        private static void AddUnknownCountRule(string word)
        {
            _uncountables.Add(word.ToLower());
        }

        private static void AddPluralRule(string rule, string replacement)
        {
            _plurals.Add(new InflectorRule(rule, replacement));
        }

        private static void AddSingularRule(string rule, string replacement)
        {
            _singulars.Add(new InflectorRule(rule, replacement));
        }

        public static string MakePlural(this string word)
        {
            return ApplyRules(_plurals, word);
        }

        public static string MakeSingular(this string word)
        {
            return ApplyRules(_singulars, word);
        }

        private static string ApplyRules(IList<InflectorRule> rules, string word)
        {
            string result = word;
            if (!_uncountables.Contains(word.ToLower()))
            {
                for (int i = rules.Count - 1; i >= 0; i--)
                {
                    string currentPass = rules[i].Apply(word);
                    if (currentPass != null)
                    {
                        result = currentPass;
                        break;
                    }
                }
            }
            return result;
        }

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

        #region Nested type: InflectorRule

        /// <summary>
        /// Summary for the InflectorRule class
        /// </summary>
        private class InflectorRule
        {
            /// <summary>
            /// 
            /// </summary>
            public readonly Regex regex;

            /// <summary>
            /// 
            /// </summary>
            public readonly string replacement;

            /// <summary>
            /// Initializes a new instance of the <see cref="InflectorRule"/> class.
            /// </summary>
            /// <param name="regexPattern">The regex pattern.</param>
            /// <param name="replacementText">The replacement text.</param>
            public InflectorRule(string regexPattern, string replacementText)
            {
                regex = new Regex(regexPattern, RegexOptions.IgnoreCase);
                replacement = replacementText;
            }

            /// <summary>
            /// Applies the specified word.
            /// </summary>
            /// <param name="word">The word.</param>
            /// <returns></returns>
            public string Apply(string word)
            {
                if (!regex.IsMatch(word))
                    return null;

                string replace = regex.Replace(word, replacement);
                if (word == word.ToUpper())
                    replace = replace.ToUpper();

                return replace;
            }
        }

        #endregion
    }
}
