using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Config;
using System.Reflection;
using FluentValidation;
using FluentValidation.Results;

namespace Simple.Validation
{
    public class ValidatorEngineFactory : AggregateFactory<ValidatorEngineFactory>, IValidatorFactory
    {
        IDictionary<Type, IValidator> _validators = new Dictionary<Type, IValidator>();


        public void Initialize(params Assembly[] assemblies)
        {
            foreach (var entry in assemblies.SelectMany(x => AssemblyScanner.FindValidatorsInAssembly(x)))
            {
                try
                {
                    _validators[entry.InterfaceType] = (IValidator)Activator.CreateInstance(entry.ValidatorType);
                }
                catch (ArgumentException)
                {
                    Simply.Do.Log(this).ErrorFormat("Couldn't create IValidator instance for type {0}", entry.InterfaceType);
                }
            }
        }



        #region IValidatorFactory Members

        public IValidator GetValidator(object obj)
        {
            if (obj == null)
                return EmptyValidator.Instance;
            else
                return GetValidator(obj.GetType());
        }

        public IValidator<T> GetValidator<T>()
        {
            return (IValidator<T>)CreateInstance(typeof(T));
        }

        public IValidator GetValidator(Type type)
        {
            return CreateInstance(type);
        }


        protected IValidator CreateInstance(Type type)
        {
            try
            {
                return _validators[typeof(IValidator<>).MakeGenericType(type)];
            }
            catch (KeyNotFoundException)
            {
                return (IValidator)Activator.CreateInstance(typeof(EmptyValidator<>).MakeGenericType(type));
            }
        }

        #endregion
    }
}
