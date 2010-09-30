using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Simple.Entities;
using Simple.Reflection;

namespace Simple
{
    public static class EntityPredicates
    {
        public static bool UniqueProperties<T>(this T model, params Expression<Func<T, object>>[] props)
           where T : class, IEntity<T>
        {
            return model.CheckPropertiesUniqueness(props as LambdaExpression[]);
        }

        public static bool CheckPropertiesUniqueness<T>(this T model, params Expression[] props)
            where T : class, IEntity<T>
        {
            return Entity<T>.Count(model.CreateUniqueExpression("x", props)) == 0;
        }

        public static Expression<Func<T, bool>> UniqueProperties<T>(this T model, string parameterName, params Expression<Func<T, object>>[] props)
            where T : class, IEntity<T>
        {
            return model.CreateUniqueExpression(parameterName, props.OfType<Expression>());
        }

        public const string NoIdentifiersFoundString = "The class must have at least one registered identifier";
        public static Expression<Func<T, bool>> CreateUniqueExpression<T>(this T model, string parameterName, IEnumerable<Expression> props)
            where T : class, IEntity<T>
        {
            var param = Expression.Parameter(typeof(T), parameterName);
            var idList = Entity<T>.Identifiers.IdentifierList;

            if (!idList.Any()) 
                throw new InvalidOperationException();
            
            var expr = idList.Select(prop =>
            {
                var property = prop.Property.GetMemberExpression(param);
                return Expression.NotEqual(property, prop.Property.EvaluateConstantFor(model));
            }).AggregateJoin((expr1, expr2) => Expression.AndAlso(expr1, expr2));

            expr = props.OfType<LambdaExpression>().Select(x =>
            {
                var prop = x.GetMemberPath();
                if (prop.Count() > 1) throw new InvalidOperationException(NoIdentifiersFoundString);
                
                var property = prop.GetMemberExpression(param);
                return Expression.Equal(property, prop.First().EvaluateConstantFor(model));
            }).Aggregate(expr, (expr1, expr2) => Expression.AndAlso(expr1, expr2));

            return Expression.Lambda<Func<T, bool>>(expr, param);
        }

        private static ConstantExpression EvaluateConstantFor<T>(this string property, T model) where T : class, IEntity<T>
        {
            var propertyInfo = typeof(T).GetProperty(property);
            var value = MethodCache.Do.GetGetter(propertyInfo)(model);
            return Expression.Constant(value, propertyInfo.PropertyType);
        }

    }
}
