using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;
using Simple.Config;

namespace Simple.Filters
{
    [DataContract]
    public class LikeExpression : TypedPropertyExpression<string>
    {
        public const string WildCardSign = "%";

        public static bool DefaultIgnoreCase
        {
            get
            {
                return SimpleLibraryConfig.Get().Business.Filters.IgnoreCaseDefault;
            }
        }

        [DataMember]
        public bool IgnoreCase { get; set; }

        public LikeExpression(PropertyName propertyName, string value, bool ignoreCase)
            : base(propertyName, value)
        {
            this.IgnoreCase = ignoreCase;
        }

        public LikeExpression(PropertyName propertyName, string value) : this(propertyName, value, DefaultIgnoreCase) { }

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
