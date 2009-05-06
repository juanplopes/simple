using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Globalization;
using System.Reflection;

namespace Simple.Common
{
    public class EmailTemplate
    {
        protected const string TypeId = "%t";
        protected const string PropertyId = "%p";
        protected const string CharId = "%c";

        protected const string ReplaceChar = "%";

        protected const string TextGroup = "text";
        protected const string FormatGroup = "format";
        protected const string IndexGroup = "index";

        protected const string EnumerableRegex = @"@%t>(?s)(?<text>.+?)<%t@";
        protected const string CommonFormatRegex = @"%c%t\.%p(:(?<format>.+))?%c";
        protected const string TitleRegex = @".title\s+""(?<text>.*)""";
        protected const string SenderRegex = @".sender\s+""(?<text>.*)""";
        protected const string MessageRegex = @".message\s*\n(?<text>(.|\n)*)";

        public string Template { get; protected set; }
        public string FinalText { get; protected set; }

        public string Title { get { return Regex.Match(FinalText, TitleRegex).Groups[TextGroup].Value; } }
        public string Sender { get { return Regex.Match(FinalText, SenderRegex).Groups[TextGroup].Value; } }
        public string Message { get { return Regex.Match(FinalText, MessageRegex).Groups[TextGroup].Value; } }

        public EmailTemplate(string template)
        {
            this.Template = template;
            this.FinalText = this.Template;
        }

        protected string GetTypeName(string type, string instance, bool escape)
        {
            return type + (string.IsNullOrEmpty(instance) ? "" : (escape ? @"\" : "") + "[" + instance + (escape ? @"\" : "") + "]");
        }

        public void AddEnumerable<T>(IEnumerable<T> enumerable) { AddEnumerable<T>(enumerable, null); }
        public void AddEnumerable<T>(IEnumerable<T> enumerable, string instance)
        {
            string typeId = GetTypeName(typeof(T).Name, instance, true);
            string newPattern = EnumerableRegex.Replace(TypeId, typeId);
            Regex regex = new Regex(newPattern);

            this.FinalText = regex.Replace(this.FinalText, delegate(Match match)
            {
                string originalText = match.Groups[TextGroup].Value;
                string resultText = string.Empty;

                foreach (T value in enumerable)
                {
                    string tempResult = originalText;
                    foreach (PropertyInfo property in typeof(T).GetProperties())
                        tempResult = ReplaceIt(tempResult, property.GetValue(value, null), ReplaceChar, typeId, property.Name);
                    resultText += tempResult;
                }

                return resultText;
            });
        }

        protected string ReplaceIt(string text, object value, string delChar, string type, string property)
        {
            string newPattern = CommonFormatRegex.Replace(CharId, delChar).Replace(TypeId, type).Replace(PropertyId, property);
            Regex regex = new Regex(newPattern);

            return regex.Replace(text, delegate(Match match)
            {
                if (value == null) return string.Empty;

                if (match.Groups[FormatGroup].Success)
                {
                    string format = match.Groups[FormatGroup].Value;
                    return string.Format(CultureInfo.InvariantCulture, "{0:" + format + "}", value);
                }
                else
                {
                    return value.ToString();
                }
            });
        }

        public void Reset()
        {
            FinalText = Template;
        }

        public void AddParameter(object parameter) { AddParameter(parameter, null); }
        public void AddParameter(object parameter, string instance)
        {
            string typeId = GetTypeName(parameter.GetType().Name, instance, true);

            foreach (PropertyInfo property in parameter.GetType().GetProperties())
                FinalText = ReplaceIt(FinalText, property.GetValue(parameter, null), ReplaceChar, typeId, property.Name);
        }

        public void AddInline(object parameter, string name)
        {
            string typeId = GetTypeName(String.Empty, name, true);
            FinalText = ReplaceIt(FinalText, parameter, ReplaceChar, typeId, "self");
        }
    }
}
