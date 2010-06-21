using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Simple.Generator
{
    public static class RegexHelper
    {
        static Regex _spaces1 = new Regex(@"\s+");
        static Regex _spaces2 = new Regex(@"(\w)([\[\(\{])");
        static Regex _spaces3 = new Regex(@"([\]\)\}])(\w)");

        public static string CorrectInput(this string x)
        {
            x = _spaces1.Replace(x, " ");
            x = _spaces2.Replace(x, EvaluateSpaces1);
            x = _spaces3.Replace(x, EvaluateSpaces2);

            return x.Trim();
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
