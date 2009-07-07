using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;

namespace Simple.Filters
{
    [Serializable]
    public class LikeExpression : TypedPropertyExpression<string>
    {
        public const string WildCardSign = "%";

        [DataMember]
        public bool IgnoreCase { get; set; }

        public LikeExpression(PropertyName propertyName, string value, bool ignoreCase)
            : base(propertyName, value)
        {
            this.IgnoreCase = ignoreCase;
        }

        public LikeExpression(PropertyName propertyName, string value) : this(propertyName, value, false) { }

        public static string ToStartsWith(string value)
        {
            return value + WildCardSign;
        }

        public static string ToEndsWith(string value)
        {
            return WildCardSign + value;
        }

        public static string ToContains(string value)
        {
            return ToStartsWith(ToEndsWith(value));
        }
    }
}
