using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace SimpleLibrary.Filters
{
    [DataContract]
    public sealed class PropertyName
    {
        public const string DotMark = ".";

        private bool dotted = false;

        [DataMember]
        public string Name { get; set; }

        public PropertyName(string name)
        {
            this.Name = name;
        }

        public static implicit operator PropertyName(string name)
        {
            return new PropertyName(name);
        }

        public static implicit operator string(PropertyName prop)
        {
            return prop.Name;
        }

        public PropertyName this[PropertyName property]
        {
            get
            {
                return this.Dot(property);
            }
        }

        public bool Dotted
        {
            get
            {
                return dotted || (dotted = Name.Contains(DotMark));
            }
        }

        public void EnsureNotDotted()
        {
            //Debug.Assert(!Dotted, "PropertyName must be non-dotted");
        }

        public PropertyName Dot(PropertyName property)
        {
            PropertyName prop = new PropertyName(this.Name + DotMark + property.Name);
            prop.dotted = true;
            return prop;
        }

        #region Operators
        public SimpleExpression Eq(object value)
        {
            return Expression.Equals(this, value);
        }
        public NotExpression NotEq(object value)
        {
            return Expression.NotEquals(this, value);
        }
        public SimpleExpression Gt(object value)
        {
            return Expression.GreaterThan(this, value);
        }
        public SimpleExpression GtEq(object value)
        {
            return Expression.GreaterThanOrEquals(this, value);
        }
        public SimpleExpression Lt(object value)
        {
            return Expression.LesserThan(this, value);
        }
        public SimpleExpression LtEq(object value)
        {
            return Expression.LesserThanOrEquals(this, value);
        }
        public LikeExpression Like(string value)
        {
            return Like(value, LikeExpression.DefaultIgnoreCase);
        }
        public LikeExpression Like(string value, bool ignoreCase)
        {
            return Expression.Like(this, value, ignoreCase);
        }
        public LikeExpression Contains(string value)
        {
            return Contains(value, LikeExpression.DefaultIgnoreCase);
        }
        public LikeExpression Contains(string value, bool ignoreCase)
        {
            return Expression.Contains(this, value, ignoreCase);
        }
        public LikeExpression StartsWith(string value)
        {
            return StartsWith(value, LikeExpression.DefaultIgnoreCase);
        }
        public LikeExpression StartsWith(string value, bool ignoreCase)
        {
            return Expression.StartsWith(this, value, ignoreCase);
        }
        public LikeExpression EndsWith(string value)
        {
            return EndsWith(value, LikeExpression.DefaultIgnoreCase);
        }
        public LikeExpression EndsWith(string value, bool ignoreCase)
        {
            return Expression.EndsWith(this, value, ignoreCase);
        }
        public BetweenExpression Between(object lo, object hi)
        {
            return Expression.Between(this, lo, hi);
        }
        public IsNullExpression IsNull()
        {
            return Expression.IsNull(this);
        }
        public IsNotNullExpression IsNotNull()
        {
            return Expression.IsNotNull(this);
        }

        #endregion
    }
}
