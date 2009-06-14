using System;
using System.Collections.Generic;

using System.Text;
using Simple.Configuration2;

namespace Simple.Config
{ 
    public class DataConfigOptionsElement : ConfigElement
    {
        [ConfigElement("mergeBeforeUpdate", Default=false)]
        public bool MergeBeforeUpdate { get; set; }
    }
}
