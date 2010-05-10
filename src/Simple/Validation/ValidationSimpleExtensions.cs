using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FluentValidation;
using FluentValidation.Results;
using Simple.Validation;

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


    }
}

