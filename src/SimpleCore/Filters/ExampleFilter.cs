using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;

namespace Simple.Filters
{
    [Serializable]
    public class ExampleFilter : Filter
    {
        [DataMember]
        public object Entity { get; set; }

        public ExampleFilter(object entity)
        {
            this.Entity = entity;
        }
    }
}
