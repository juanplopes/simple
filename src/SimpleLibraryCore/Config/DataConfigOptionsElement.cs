using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicLibrary.Configuration;

namespace SimpleLibrary.Config
{ 
    public class DataConfigOptionsElement : ConfigElement
    {
        [ConfigElement("mergeBeforeUpdate", Default=false)]
        public bool MergeBeforeUpdate { get; set; }
    }
}
