using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Validation;
using System.Linq.Expressions;
using System.Reflection;
using Simple.Expressions;
using Simple.Entities;
using FluentValidation.Results;
using FluentValidation;
using FluentValidation.Internal;
using Simple.Common;

namespace Simple
{
    public static class ValidationSimpleExtensions
    {
        private static ValidatorEngineFactory Factory(object key)
        {
            return ValidatorEngineFactory.Do[key];
        }

        public static ValidationList ToValidationList(this ValidationResult results)
        {
            return new ValidationList(results.Errors
                .Select(x => new ValidationItem(x.PropertyName, x.ErrorMessage, x.AttemptedValue)));
        }

        public static ValidationList Validate(this Simply simply, object obj)
        {
            return Factory(simply.ConfigKey).GetValidator(obj).Validate(obj).ToValidationList();
        }

        public static ValidationList Validate<T>(this Simply simply, T obj)
        {
            return Factory(simply.ConfigKey).GetValidator<T>().Validate(obj).ToValidationList();
        }

        public static ValidationList Validate<T>(this Simply simply, T obj, params string[] props)
        {
            return Factory(simply.ConfigKey).GetValidator<T>().Validate(obj, props).ToValidationList();
        }


        public static ValidationList Validate<T>(this Simply simply, T obj, params Expression<Func<T, object>>[] props)
            where T : class
        {
            return Factory(simply.ConfigKey).GetValidator<T>().Validate(obj, props).ToValidationList();
        }

        public static void AndThrow(this ValidationList result)
        {
            if (!result.IsValid)
            {
                throw new SimpleValidationException(result);
            }
        }

        public static SimplyConfigure Validator(this SimplyConfigure config, params Assembly[] assemblies)
        {
            Factory(config.ConfigKey).Initialize(assemblies);
            return config;
        }

        #region FluentValidation Simple Extensions

        //public static IRuleBuilderOptions<T, TProp> MustBeUnique<T, TProp>(this IRuleBuilderInitial<T, TProp> builder)
        //    where T : class, IEntity<T>
        //{
        //    var ruleBuilder = builder as RuleBuilder<T, TProp>;

        //    return builder.Must((T model, TProp prop) => model.UniqueProperties<T>(ruleBuilder.Model.Expression));
        //}

        public static bool UniqueProperties<T>(this T model, params  Expression<Func<T, object>>[] props)
           where T : class, IEntity<T>
        {
            return model.UniqueProperties(props as LambdaExpression[]);
        }

        public static bool UniqueProperties<T>(this T model, params Expression[] props)
            where T : class, IEntity<T>
        {
            return Entity<T>.Count(props.CreateUniqueExpression<T>(model, "x")) == 0;
        }

        public static Expression<Func<T, bool>> CreateUniqueExpression<T>(this IEnumerable<Expression> props, T model, string parameterName) 
            where T : class, IEntity<T>
        {
            var param = Expression.Parameter(typeof(T), parameterName);
            var idList = Entity<T>.Identifiers.IdentifierList;

            var expr = idList.Select(prop =>
            {
                var idExpr = prop.GetPropertyExpression(param);
                var idLambda = Expression.Lambda(idExpr, param);
                return Expression.NotEqual(idExpr, Expression.Constant(idLambda.Compile().DynamicInvoke(model)));
            }).AggregateJoin((expr1, expr2) => Expression.AndAlso(expr1, expr2));

            expr = props.OfType<LambdaExpression>().Select(x => x.GetMemberName().GetPropertyExpression(param))
                .Aggregate(expr, (seed, x) => Expression.AndAlso(seed, x));

            var lambda = Expression.Lambda<Func<T, bool>>(expr, param);
            return lambda;
        }

        #endregion
    }
}

