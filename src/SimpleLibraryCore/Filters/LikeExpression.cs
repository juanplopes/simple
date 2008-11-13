using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;
using SimpleLibrary.Config;

namespace SimpleLibrary.Filters
{
    [DataContract]
    public class LikeExpression : PropertyExpression<string>
    {
        public static bool DefaultIgnoreCase
        {
            get
            {
                return SimpleLibraryConfig.Get().Business.Filters.IgnoreCaseDefault;
            }
        }

        [DataMember]
        public bool IgnoreCase { get; set; }

        public LikeExpression(string propertyName, string value, bool ignoreCase)
            : base(propertyName, value)
        {
            this.IgnoreCase = ignoreCase;
        }
    }
}
