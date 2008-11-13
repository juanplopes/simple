using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;

namespace SimpleLibrary.Filters
{
    [DataContract]
    public class SimpleExpression : PropertyExpression<object>
    {
        public const string EqualsExpression = "=";
        public const string GreaterThanExpression = ">";
        public const string LesserThanExpression = "<";
        public const string GreaterThanOrEqualsExpression = ">=";
        public const string LesserThanOrEqualsExpression = "<=";

        [DataMember]
        public string Operator { get; set; }

        public SimpleExpression(string propertyName, object value, string op) : base(propertyName, value)
        {
            Operator = op;
        }
    }
}
