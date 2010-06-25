using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Generator.Strings
{
    public interface IPluralizer
    {
        string ToPlural(string word);
        string ToSingular(string word);
    }
}
