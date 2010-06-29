using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Simple.Generator.Parsers
{
    public interface ICommandParser
    {
        string Name { get; }
        MemberExpression Member { get; }
        Regex RegularExpression { get; }

        string Parse(string args, ICommand generator);
    }

}
