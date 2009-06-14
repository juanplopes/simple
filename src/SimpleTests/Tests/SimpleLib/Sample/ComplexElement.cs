using System;
using System.Collections.Generic;
using System.Text;
using Simple.Configuration2;

namespace Simple.Tests.SimpleLib.Sample
{
    public class ComplexElement : ConfigElement
    {
        [ConfigElement("list1")]
        public List<BasicTypesElement> List1 { get; set; }

        [ConfigElement("dic1")]
        public Dictionary<int, BasicTypesElement> Dic1 { get; set; }
    }
}
