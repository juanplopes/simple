using System;
using System.Collections.Generic;

using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace SimpleLibrary.DataAccess
{
    public class CriteriaHelper
    {
        public static Order GetOrder(Filters.OrderBy order)
        {
            if (order.IsAsc)
                return Order.Asc(order.PropertyName);
            else
                return Order.Desc(order.PropertyName);
        }

        public static ICriterion GetCriterion(Filters.Filter filter)
        {
            if (filter is Filters.SimpleExpression)
            {
                Filters.SimpleExpression expression = filter as Filters.SimpleExpression;
                return new SimpleExpression(expression.PropertyName, expression.Value, expression.Operator);
            }
            else if (filter is Filters.LikeExpression)
            {
                Filters.LikeExpression like = filter as Filters.LikeExpression;
                return new LikeExpression(like.PropertyName, like.Value, MatchMode.Exact, null, like.IgnoreCase);
            }
            else if (filter is Filters.ExampleFilter)
            {
                Filters.ExampleFilter example = filter as Filters.ExampleFilter;
                return Example.Create(example.Entity);
            }
            else if (filter is Filters.UnaryOperator)
            {
                ICriterion criterion1 = GetCriterion((filter as Filters.UnaryOperator).Filter1);
                if (filter is Filters.BinaryOperator)
                {
                    ICriterion criterion2 = GetCriterion((filter as Filters.BinaryOperator).Filter2);

                    if (filter is Filters.AndExpression)
                    {
                        return new AndExpression(criterion1, criterion2);
                    }
                    else if (filter is Filters.OrExpression)
                    {
                        return new OrExpression(criterion1, criterion2);
                    }
                }
                else if (filter is Filters.NotExpression)
                {
                    return new NotExpression(criterion1);
                }
            }
            else if (filter is Filters.BetweenExpression)
            {
                Filters.BetweenExpression between = filter as Filters.BetweenExpression;
                return new BetweenExpression(between.PropertyName, between.LoValue, between.HiValue);
            }
            else if (filter is Filters.BooleanExpression)
            {
                Filters.BooleanExpression boolean = filter as Filters.BooleanExpression;
                return Expression.Eq(new ConstantProjection(1), boolean.Value ? 1 : 0);
            }
            else if (filter is Filters.PropertyExpression)
            {
                Filters.PropertyExpression prop = filter as Filters.PropertyExpression;
                if (prop is Filters.IsNullExpression) return Expression.IsNull(prop.PropertyName);
                if (prop is Filters.IsNotNullExpression) return Expression.IsNotNull(prop.PropertyName);
            }
            throw new InvalidOperationException("Invalid filter type");
        }
    }
}
