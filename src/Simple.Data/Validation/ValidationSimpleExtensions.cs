using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Validator.Engine;
using Simple.Validation;
using System.Linq.Expressions;
using System.Reflection;
using NHibernate.Validator.Cfg.Loquacious;
using Simple.Expressions;
using Simple.Entities;

namespace Simple
{
    public static class ValidationSimpleExtensions
    {
        public static IChainableConstraint<IInstanceConstraints<T>> PropertyBy<T>(this IInstanceConstraints<T> self, Expression<Func<T, object>> expr, Predicate<T> validator)
            where T : class
        {
            return self.By((t, context) =>
            {
                if (!validator(t))
                {
                    context.AddInvalid(context.DefaultErrorMessage, ExpressionHelper.GetMemberName(expr));
                    context.DisableDefaultError();
                    return false;
                }
                else return true;
            });
        }

        private static ValidatorEngineFactory Factory(object key)
        {
            return ValidatorEngineFactory.Do[key];
        }

        public static IList<ValidationItem> Validate(this Simply simply, object obj)
        {
            return Factory(simply.ConfigKey).Validator.Validate(obj).ToValidationErrors();
        }

        public static IList<ValidationItem> Validate(this Simply simply, Type type, string propName, object value)
        {
            return Factory(simply.ConfigKey).Validator.ValidatePropertyValue(type, propName, value).ToValidationErrors();
        }
        public static IList<ValidationItem> Validate<T, P>(this Simply simply, T obj, Expression<Func<T, P>> expr)
            where T : class
        {
            return Factory(simply.ConfigKey).Validator.ValidatePropertyValue(obj, expr).ToValidationErrors();
        }

        public static void AndThrow(this IList<ValidationItem> values)
        {
            values.AndThrow(null);
        }

        public static void AndThrow(this IList<ValidationItem> values, string baseName)
        {
            if (values.Count > 0)
            {
                if (!string.IsNullOrEmpty(baseName))
                    values = values.Select(x => x.RootedBy(baseName)).ToList();

                throw new ValidationException(values);
            }
        }

        public static IList<ValidationItem> ToValidationErrors(this IList<InvalidValue> list)
        {
            return list.Select(x=>new ValidationItem(x.Message, x.PropertyName, x.PropertyPath)).ToList();
        }

        public static SimplyConfigure Validator(this SimplyConfigure config, params Assembly[] assemblies)
        {
            Factory(config.ConfigKey).Initialize(assemblies);
            return config;
        }


    }
}

