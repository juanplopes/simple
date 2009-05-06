using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Generator.RulesInterfaces
{
    public class InterfaceCandidate
    {
        public string ClassName { get; set; }
        public List<string> Inherits { get; set; }

        public List<string> MethodSignatures { get; set; }

        public InterfaceCandidate()
        {
            Inherits = new List<string>();
            MethodSignatures = new List<string>();
        }
    }
}
