using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Simple.Generator.Parsers
{
    public class Regexes
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


    }
}
