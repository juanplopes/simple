using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleLibrary.Filters
{
    public static class PropertyFilter
    {
        public static SimpleExpression Equals(this string propertyName, object value)
        {
            return new SimpleExpression(propertyName, value, SimpleExpression.EqualsExpression);
        }
        public static SimpleExpression GreaterThan(this string propertyName, object value)
        {
            return new SimpleExpression(propertyName, value, SimpleExpression.GreaterThanExpression);
        }
        public static SimpleExpression GreaterThanOrEquals(this string propertyName, object value)
        {
            return new SimpleExpression(propertyName, value, SimpleExpression.GreaterThanOrEqualsExpression);
        }
        public static SimpleExpression LesserThan(this string propertyName, object value)
        {
            return new SimpleExpression(propertyName, value, SimpleExpression.LesserThanExpression);
        }
        public static SimpleExpression LesserThanOrEquals(this string propertyName, object value)
        {
            return new SimpleExpression(propertyName, value, SimpleExpression.LesserThanOrEqualsExpression);
        }
        public static LikeExpression Like(this string propertyName, string value)
        {
            return new LikeExpression(propertyName, value);
        }
        public static LikeExpression Contains(this string propertyName, string value)
        {
            return Like(propertyName, "%" + value + "%");
        }
        public static LikeExpression StartsWith(this string propertyName, string value)
        {
            return Like(propertyName, value + "%");
        }
        public static LikeExpression EndsWith(this string propertyName, string value)
        {
            return Like(propertyName, "%" + value);
        }
        public static BetweenExpression Between(this string propertyName, object lo, object hi)
        {
            return new BetweenExpression(propertyName, lo, hi);
        }
        public static BooleanExpression Boolean(bool value)
        {
            return new BooleanExpression(value);
        }
        public static ExampleFilter Example(object entity)
        {
            return new ExampleFilter(entity);
        }
    }
}
