using System;
using System.Collections.Generic;
using System.Text;
using BasicLibrary.Configuration;

namespace ACN.Order.BusinessLogic.Config
{
    public class CreditCheckPropertyGroupElement : ConfigElement
    {
        public enum FromEnum
        {
            Customer=0,
            Location=1,
            Service=2
        }

        [ConfigElement("property")]
        public List<string> Properties { get; set; }

        [ConfigElement("from")]
        public FromEnum From { get; set; }

    }
}
