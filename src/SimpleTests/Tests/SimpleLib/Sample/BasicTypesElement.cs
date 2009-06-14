using System;
using System.Collections.Generic;
using System.Text;
using Simple.Configuration2;

namespace Simple.Tests.SimpleLib.Sample
{
    public class BasicTypesElement : ConfigElement
    {
        [ConfigElement("intValue")]
        public int IntValue { get; set; }

        [ConfigElement("doubleValue")]
        public double DoubleValue { get; set; }

        [ConfigElement("dateValue")]
        public DateTime DateValue { get; set; }

        [ConfigElement("decimalValue")]
        public decimal DecimalValue { get; set; }
    }
}
