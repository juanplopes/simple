using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Simple.Generator.Misc
{
    public class CSharpInterfaceReplacer
    {
        public string ReplaceHide(string content, string fromName, string toName)
        {
            var regex = new Regex(
                @"class[^{]+:([^{,]*,)*\s*(?<test>[^{<>]*%s)".Replace("%s", fromName), RegexOptions.IgnorePatternWhitespace);
            return regex.Replace(content, match =>
            {
                var group = match.Groups["test"];
                return match.Value.Remove(group.Index - match.Index) + toName + "<" + group.Value + ">"
                    + match.Value.Substring(group.Index + group.Length - match.Index);
                   
            });
        }

        public string ReplaceShow(string content, string fromName)
        {
            var regex = new Regex(
                @"class[^{]+:([^{,]*,)*\s*(?<test>[^{<>]*%s<(?<inner>[^>]+)>)".Replace("%s", fromName), RegexOptions.IgnorePatternWhitespace);

            while (regex.IsMatch(content))
            {

                content = regex.Replace(content, match =>
                {
                    var group = match.Groups["test"];
                    var groupInner = match.Groups["inner"];
                    return match.Value.Remove(group.Index - match.Index) + groupInner.Value
                        + match.Value.Substring(group.Index + group.Length - match.Index);

                });
            }
            return content;
        }
    }
}
