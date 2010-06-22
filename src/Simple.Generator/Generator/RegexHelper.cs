using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Simple.Generator
{
    public static class RegexHelper
    {
        public const string ValueGroup = "value";

        private const string ItemRegexString = @"\s*((?<value>[^()""'\s,]+)|([""'](?<value>.*?)[""']))\s*";
        private const string ListRegexString = @"\(?($item$,)*($item$)\)?";
        private const string OptionRegexString = @"\s*((?<value>[+-])$name$|$name$\s*[\s:=]\s*$list$)\s*";

        private static string GetListString()
        {
            return ListRegexString.Replace("$item$", ItemRegexString);
        }

        private static string GetOptionString(string name)
        {
            return OptionRegexString.Replace("$list$", GetListString()).Replace("$name$", name);
        }

        private static Regex _listRegex = new Regex(GetListString().ToRegexFormat(true), RegexOptions.Compiled);
        public static Regex ListRegex { get { return _listRegex; } }

        public static Regex OptionRegex(string name)
        {
            return new Regex(GetOptionString(name), RegexOptions.Compiled);
        }


        static Regex _spaces1 = new Regex(@"\s+");
        static Regex _spaces2 = new Regex(@"(\w)([\[\(\{])");
        static Regex _spaces3 = new Regex(@"([\]\)\}])(\w)");

        public static string CorrectInput(this string x)
        {
            x = _spaces1.Replace(x, " ");
            x = _spaces2.Replace(x, EvaluateSpaces1);
            x = _spaces3.Replace(x, EvaluateSpaces2);

            return x;
        }

        public static string ToRegexFormat(this string x, bool mustBeFirst)
        {
            return (mustBeFirst ? "^" : "") + @"\s*" + x + @"(\s|$)";
        }

        private static string EvaluateSpaces1(Match match)
        {
            return match.Groups[1] + " " + match.Groups[2];
        }

        private static string EvaluateSpaces2(Match match)
        {
            return match.Groups[1] + " " + match.Groups[2];
        }
    }
}
