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
        public LikeExpression(string propertyName, string value) : base(propertyName, value)
        {
 
        }
    }
}
