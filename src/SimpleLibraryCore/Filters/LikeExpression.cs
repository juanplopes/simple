using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SimpleLibrary.Filters
{
    [DataContract]
    public class LikeExpression : PropertyExpression<string>
    {
        public const bool DefaultIgnoreCase = false;

        [DataMember]
        public bool IgnoreCase { get; set; }

        public LikeExpression(string propertyName, string value, bool ignoreCase) : base(propertyName, value)
        {
            this.IgnoreCase = ignoreCase;
        }
    }
}
