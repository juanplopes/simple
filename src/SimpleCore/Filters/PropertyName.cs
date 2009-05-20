using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.Collections;

namespace Simple.Filters
{
    [Serializable]
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
        public NotExpression NotEq(object value)
        {
            return new NotExpression(this.Eq(value));
        }
        public InExpression In(params object[] values)
        {
            return new InExpression(this, values);
        }
        public InExpression In(ICollection values)
        {
            return new InExpression(this, values);
        }

        public SimpleExpression Eq(object value)
        {
            return new SimpleExpression(this, value, SimpleExpression.EqualsExpression);
        }
        public SimpleExpression Gt(object value)
        {
            return new SimpleExpression(this, value, SimpleExpression.GreaterThanExpression);
        }
        public SimpleExpression GtEq(object value)
        {
            return new SimpleExpression(this, value, SimpleExpression.GreaterThanOrEqualsExpression);
        }
        public SimpleExpression Lt(object value)
        {
            return new SimpleExpression(this, value, SimpleExpression.LesserThanExpression);
        }
        public SimpleExpression LtEq(object value)
        {
            return new SimpleExpression(this, value, SimpleExpression.LesserThanOrEqualsExpression);
        }
        public LikeExpression Like(string value)
        {
            return Like(value, LikeExpression.DefaultIgnoreCase);
        }
        public LikeExpression Like(string value, bool ignoreCase)
        {
            return new LikeExpression(this, value, ignoreCase);
        }
        public LikeExpression Contains(string value)
        {
            return Contains(value, LikeExpression.DefaultIgnoreCase);
        }
        public LikeExpression Contains(string value, bool ignoreCase)
        {
            return Like(LikeExpression.ToContains(value), ignoreCase);
        }
        public LikeExpression StartsWith(string value)
        {
            return StartsWith(value, LikeExpression.DefaultIgnoreCase);
        }
        public LikeExpression StartsWith(string value, bool ignoreCase)
        {
            return Like(LikeExpression.ToStartsWith(value), ignoreCase);
        }
        public LikeExpression EndsWith(string value)
        {
            return EndsWith(value, LikeExpression.DefaultIgnoreCase);
        }
        public LikeExpression EndsWith(string value, bool ignoreCase)
        {
            return Like(LikeExpression.ToEndsWith(value), ignoreCase);
        }
        public BetweenExpression Between(object lo, object hi)
        {
            return new BetweenExpression(this, lo, hi);
        }
        public IsNullExpression IsNull()
        {
            return new IsNullExpression(this);
        }
        public IsNotNullExpression IsNotNull()
        {
            return new IsNotNullExpression(this);
        }

        #endregion
    }
}
