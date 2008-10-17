using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleLibrary.Filters
{
    public static class PropertyFilter
    {
        public static SimpleExpression Eq(this PropertyName propertyName, object value)
        {
            return Expression.Equals(propertyName, value);
        }
        public static SimpleExpression Gt(this PropertyName propertyName, object value)
        {
            return Expression.GreaterThan(propertyName, value);
        }
        public static SimpleExpression GtEq(this PropertyName propertyName, object value)
        {
            return Expression.GreaterThanOrEquals(propertyName, value);
        }
        public static SimpleExpression Lt(this PropertyName propertyName, object value)
        {
            return Expression.LesserThan(propertyName, value);
        }
        public static SimpleExpression LtEq(this PropertyName propertyName, object value)
        {
            return Expression.LesserThanOrEquals(propertyName, value);
        }
        public static LikeExpression Like(this PropertyName propertyName, string value)
        {
            return Expression.Like(propertyName, value);
        }
        public static LikeExpression Contains(this PropertyName propertyName, string value)
        {
            return Expression.Contains(propertyName, value);
        }
        public static LikeExpression StartsWith(this PropertyName propertyName, string value)
        {
            return Expression.StartsWith(propertyName, value);
        }
        public static LikeExpression EndsWith(this PropertyName propertyName, string value)
        {
            return Expression.EndsWith(propertyName, value);
        }
        public static BetweenExpression Between(this PropertyName propertyName, object lo, object hi)
        {
            return Expression.Between(propertyName, lo, hi);
        }
    }
}
