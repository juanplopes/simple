using System;
using System.Collections.Generic;
using System.Text;
using DDW;

namespace SimpleGenerator.RulesInterfaces
{
    public class SourceHelper
    {
        public static string GetSource(ISourceCode source)
        {
            StringBuilder b = new StringBuilder();
            source.ToSource(b);
            return b.ToString();
        }

        public static string GetSource(IType source)
        {
            StringBuilder b = new StringBuilder();
            source.ToSource(b);
            return b.ToString();
        }
    }
}
