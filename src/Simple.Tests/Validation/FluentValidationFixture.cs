using System;
using System.Linq;
using FluentValidation;
using NUnit.Framework;
using SharpTestsEx;
using Simple.Tests.Validation.ValidationResources;


namespace Simple.Tests.Validation
{
    public class Validator<T> : AbstractValidator<T>
    {

    }

    public class FluentValidationMessages
    {
        public string NotEmpty(string propName)
        {
            return FluentValidation.Resources.Messages.notempty_error.Replace("{PropertyName}", propName);
        }

        public string NotNull(string propName)
        {
            return FluentValidation.Resources.Messages.notnull_error.Replace("{PropertyName}", propName);
        }

        public string GreaterThan(string propName, string value)
        {
            return FluentValidation.Resources.Messages.greaterthan_error
                .Replace("{PropertyName}", propName)
                .Replace("{ComparisonValue}", value);
        }

        public string GreaterThanOrEqualTo(string propName, string value)
        {
            return FluentValidation.Resources.Messages.greaterthanorequal_error
                .Replace("{PropertyName}", propName)
                .Replace("{ComparisonValue}", value);
        }

        public string LessThan(string propName, string value)
        {
            return FluentValidation.Resources.Messages.lessthan_error
                .Replace("{PropertyName}", propName)
                .Replace("{ComparisonValue}", value);
        }

        public string LessThanOrEqualTo(string propName, string value)
        {
            return FluentValidation.Resources.Messages.lessthanorequal_error
                .Replace("{PropertyName}", propName)
                .Replace("{ComparisonValue}", value);
        }

        public string InclusiveBetween(string propName, string min, string max, string total)
        {
            return FluentValidation.Resources.Messages.inclusivebetween_error
                .Replace("{PropertyName}", propName)
                .Replace("{From}", min)
                .Replace("{To}", max)
                .Replace("{Value}", total);
        }
    }

    public class FluentValidationFixture
    {
        public FluentValidationMessages Messages { get { return new FluentValidationMessages(); } }

        [Test]
        public void FieldIsNotNull()
        {
            var validator = new Validator<Company>();
            validator.RuleFor(x => x.Name).NotNull();
            validator.RuleFor(x => x.CompanyValue).NotNull();

            var model = new Company() { };

            validator.Validate(model).IsValid.Should().Be.False();
            Messages.NotNull("Name").Should().Be(validator.Validate(model).Errors[0].ErrorMessage);
            Messages.NotNull("Company Value").Should().Be(validator.Validate(model).Errors[1].ErrorMessage);

            model = new Company() { Name = "", CompanyValue = new Decimal() };
            validator.Validate(model).IsValid.Should().Be.True();
        }

        [Test]
        public void FieldIsNotEmptyValidation()
        {
            var validator = new Validator<User>();
            validator.RuleFor(x => x.Name).NotEmpty();

            var model = new User() { Name = "" };

            validator.Validate(model).IsValid.Should().Be.False();
            Messages.NotEmpty("Name").Should().Be(validator.Validate(model).Errors.First().ErrorMessage);

            model = new User() { Name = "Stevie" };
            validator.Validate(model).IsValid.Should().Be.True();
        }

        [Test]
        public void FieldIsNotEmptyValidationWithDefaultValues()
        {
            var validator = new Validator<User>();
            validator.RuleFor(x => x.Id).NotEmpty();
            validator.RuleFor(x => x.BirthDate).NotEmpty();

            var model = new User() { Id = new Int32(), BirthDate = new DateTime() };

            validator.Validate(model).IsValid.Should().Be.False();
            Messages.NotEmpty("Id").Should().Be(validator.Validate(model).Errors[0].ErrorMessage);
            Messages.NotEmpty("Birth Date").Should().Be(validator.Validate(model).Errors[1].ErrorMessage);

            model = new User() { Id = 3, BirthDate = DateTime.Now };
            validator.Validate(model).IsValid.Should().Be.True();
        }

        [Test]
        public void FieldIsGreatarThen()
        {
            var validator = new Validator<User>();
            var date = new DateTime(1920, 1, 1);
            validator.RuleFor(x => x.BirthDate).GreaterThan(date);

            var model = new User() { BirthDate = date };

            validator.Validate(model).IsValid.Should().Be.False();
            Messages.GreaterThan("Birth Date", date.ToString()).Should().Be(validator.Validate(model).Errors[0].ErrorMessage);

            model = new User() { BirthDate = DateTime.Now };

            validator.Validate(model).IsValid.Should().Be.True();
        }

        [Test]
        public void FieldIsGreaterThanOrEqual()
        {
            var validator = new Validator<User>();
            var date = new DateTime(1920, 1, 1);
            validator.RuleFor(x => x.BirthDate).GreaterThanOrEqualTo(date);

            var model = new User() { BirthDate = new DateTime(1919, 1, 1) };

            validator.Validate(model).IsValid.Should().Be.False();
            Messages.GreaterThanOrEqualTo("Birth Date", date.ToString()).Should().Be(validator.Validate(model).Errors[0].ErrorMessage);

            model = new User() { BirthDate = date };

            validator.Validate(model).IsValid.Should().Be.True();
        }

        [Test]
        public void FieldIsLessThan()
        {
            var validator = new Validator<User>();
            var date = new DateTime(1920, 1, 1);
            validator.RuleFor(x => x.BirthDate).LessThan(date);

            var model = new User() { BirthDate = date };

            validator.Validate(model).IsValid.Should().Be.False();
            Messages.LessThan("Birth Date", date.ToString()).Should().Be(validator.Validate(model).Errors[0].ErrorMessage);

            model = new User() { BirthDate = new DateTime(1900, 1, 1) };

            validator.Validate(model).IsValid.Should().Be.True();
        }

        [Test]
        public void FieldIsLessThanOrEqual()
        {
            var validator = new Validator<User>();
            var date = new DateTime(1920, 1, 1);
            validator.RuleFor(x => x.BirthDate).LessThanOrEqualTo(date);

            var model = new User() { BirthDate = new DateTime(1921, 1, 1) };

            validator.Validate(model).IsValid.Should().Be.False();
            Messages.LessThanOrEqualTo("Birth Date", date.ToString()).Should().Be(validator.Validate(model).Errors[0].ErrorMessage);

            model = new User() { BirthDate = date };

            validator.Validate(model).IsValid.Should().Be.True();
        }

        [Test]
        public void FieldIsInclusiveBetween()
        {
            var validator = new Validator<User>();

            var startDate = new DateTime(1920, 1, 1);
            var endDate = DateTime.Now;

            validator.RuleFor(x => x.BirthDate).InclusiveBetween(startDate, endDate);

            var model = new User() { BirthDate = startDate.AddSeconds(-1) };

            validator.Validate(model).IsValid.Should().Be.False();
            Messages.InclusiveBetween("Birth Date", startDate.ToString(), endDate.ToString(), startDate.AddSeconds(-1).ToString()).Should().Be(validator.Validate(model).Errors[0].ErrorMessage);

            model = new User() { BirthDate = startDate };

            validator.Validate(model).IsValid.Should().Be.True();
        }

        [Test]        
        public void FieldIsAnEntity()
        {
            var userValidator = new Validator<User>();
            var companyValidator = new Validator<Company>();

            var company = new Company();

            companyValidator.RuleFor(x => x.CompanyValue).NotEmpty();
            userValidator.RuleFor(x => x.Company).SetValidator(companyValidator);

            var model = new User() { Company = company };

            userValidator.Validate(model).IsValid.Should().Be.False();
            Messages.NotEmpty("Company Value").Should().Be(userValidator.Validate(model).Errors[0].ErrorMessage);
            "Company.CompanyValue".Should().Be(userValidator.Validate(model).Errors[0].PropertyName);

            model = new User() { Company = new Company() { CompanyValue = 20 } };

            userValidator.Validate(model).IsValid.Should().Be.True();
        }
    }


    //RuleFor(x => x.Name).NotEmpty();
    //RuleFor(x => x.BirthDate).GreaterThanOrEqualTo(new DateTime(1920, 1, 1));
    //RuleFor(x => x.Picture).Must(x => { return (x == null) || (x.Length < 50); });
    //RuleFor(x => x.Company).Cascade();

    //RuleFor(x => x.Name).NotEmpty();
    //RuleFor(x => x.CompanyValue).GreaterThan(0);
    //RuleFor(x => x.Owner).Length(0, 25);
    //RuleFor(x => x.Address).Length(5, 500);


}
