using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;
using Simple.Patterns;

namespace Simple.Validation
{
    public class EmptyValidator : Singleton<EmptyValidator>, IValidator
    {
        #region IValidator Members

        public IValidatorDescriptor CreateDescriptor()
        {
            return EmptyValidatorDescriptor.Instance;
        }

        public FluentValidation.Results.ValidationResult Validate(object instance)
        {
            return new ValidationResult(new ValidationFailure[0]);
        }

        #endregion
    }

    public class EmptyValidator<T> : EmptyValidator, IValidator<T>
    {

        #region IValidator<T> Members

        public ValidationResult Validate(ValidationContext<T> context)
        {
            return Validate((object)null);
        }

        public ValidationResult Validate(T instance)
        {
            return Validate((object)null);
        }

        #endregion

        #region IEnumerable<IValidationRule<T>> Members

        public IEnumerator<IValidationRule<T>> GetEnumerator()
        {
            yield break;
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            yield break;
        }

        #endregion
    }

    public class EmptyValidatorDescriptor : Singleton<EmptyValidatorDescriptor>, IValidatorDescriptor
    {

        #region IValidatorDescriptor Members

        public ILookup<string, IPropertyValidator> GetMembersWithValidators()
        {
            return new IPropertyValidator[0].ToLookup(x => x.ToString());
        }

        public string GetName(string property)
        {
            return property;
        }

        public IEnumerable<FluentValidation.Validators.IPropertyValidator> GetValidatorsForMember(string name)
        {
            yield break;
        }

        #endregion
    }
}
