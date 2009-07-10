using System;
using System.Collections.Generic;

using System.Text;
using BasicLibrary.Configuration;

namespace SimpleLibrary.Config
{
    public class FiltersElement : ConfigElement
    {
        [ConfigElement("ignoreCaseDefault", Default = false)]
        public bool IgnoreCaseDefault { get; set; }
    }
}
