using System;
using System.Collections.Generic;
using System.Text;
using BasicLibrary.Configuration;

namespace ACN.Order.BusinessLogic.Config
{
    public class ProvisionElement: ConfigElement
    {
        [ConfigElement("Language")]
        public string Language { get; set; }

        [ConfigElement("TimeZone")]
        public string TimeZone { get; set; }

    }
}
