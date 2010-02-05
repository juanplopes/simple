using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Validator.Engine;
using Simple.Validation;
using System.Linq.Expressions;
using System.Reflection;

namespace Simple
{
    public static class ValidationSimpleExtensions
    {
        private static ValidatorEngineFactory Factory(object key)
        {
            return ValidatorEngineFactory.Do[key];
        }

        public static InvalidValue[] Validate(this Simply simply, object obj)
        {
            return Factory(simply.ConfigKey).Validator.Validate(obj);
        }

        public static InvalidValue[] Validate(this Simply simply, Type type, string propName, object value)
        {
            return Factory(simply.ConfigKey).Validator.ValidatePropertyValue(type, propName, value);
        }
        public static InvalidValue[] Validate<T, P>(this Simply simply, T obj, Expression<Func<T, P>> expr)
            where T : class
        {
            return Factory(simply.ConfigKey).Validator.ValidatePropertyValue(obj, expr);
        }

        public static void AndThrow(this InvalidValue[] values)
        {
            values.AndThrow(null);
        }

        public static void AndThrow(this InvalidValue[] values, string baseName)
        {
            var newValues = values;
            if (!string.IsNullOrEmpty(baseName))
                newValues = values.Select(x => x.FluentlyDo(y => y.AddParentEntity(null, baseName))).ToArray();

            if (values.Length > 0)
                throw new ValidationException(values);
        }

        public static SimplyConfigure Validator(this SimplyConfigure config, params Assembly[] assemblies)
        {
            Factory(config.ConfigKey).Initialize(assemblies);
            return config;
        }


    }
}

