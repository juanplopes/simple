using System;
using System.Collections.Generic;

using System.Text;
using Simple.Configuration;

namespace Simple.Config
{
    public class FiltersElement : ConfigElement
    {
        [ConfigElement("ignoreCaseDefault", Default = false)]
        public bool IgnoreCaseDefault { get; set; }
    }
}
