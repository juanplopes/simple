using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Tests.Validation.ValidationResources;
using FluentValidation;


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

            Assert.IsFalse(validator.Validate(model).IsValid);
            Assert.AreEqual(validator.Validate(model).Errors[0].ErrorMessage, Messages.NotNull("Name"));
            Assert.AreEqual(validator.Validate(model).Errors[1].ErrorMessage, Messages.NotNull("Company Value"));

            model = new Company() { Name = "", CompanyValue = new Decimal() };
            Assert.IsTrue(validator.Validate(model).IsValid);
        }

        [Test]
        public void FieldIsNotEmptyValidation()
        {
            var validator = new Validator<User>();
            validator.RuleFor(x => x.Name).NotEmpty();

            var model = new User() { Name = "" };

            Assert.IsFalse(validator.Validate(model).IsValid);
            Assert.AreEqual(validator.Validate(model).Errors.First().ErrorMessage, Messages.NotEmpty("Name"));

            model = new User() { Name = "Stevie" };
            Assert.IsTrue(validator.Validate(model).IsValid);
        }

        [Test]
        public void FieldIsNotEmptyValidationWithDefaultValues()
        {
            var validator = new Validator<User>();
            validator.RuleFor(x => x.Id).NotEmpty();
            validator.RuleFor(x => x.BirthDate).NotEmpty();

            var model = new User() { Id = new Int32(), BirthDate = new DateTime() };

            Assert.IsFalse(validator.Validate(model).IsValid);
            Assert.AreEqual(validator.Validate(model).Errors[0].ErrorMessage, Messages.NotEmpty("Id"));
            Assert.AreEqual(validator.Validate(model).Errors[1].ErrorMessage, Messages.NotEmpty("Birth Date"));

            model = new User() { Id = 3, BirthDate = DateTime.Now };
            Assert.IsTrue(validator.Validate(model).IsValid);
        }

        [Test]
        public void FieldIsGreatarThen()
        {
            var validator = new Validator<User>();
            var date = new DateTime(1920, 1, 1);
            validator.RuleFor(x => x.BirthDate).GreaterThan(date);

            var model = new User() { BirthDate = date };

            Assert.IsFalse(validator.Validate(model).IsValid);
            Assert.AreEqual(validator.Validate(model).Errors[0].ErrorMessage, Messages.GreaterThan("Birth Date", date.ToString()));

            model = new User() { BirthDate = DateTime.Now };

            Assert.IsTrue(validator.Validate(model).IsValid);
        }

        [Test]
        public void FieldIsGreaterThanOrEqual()
        {
            var validator = new Validator<User>();
            var date = new DateTime(1920, 1, 1);
            validator.RuleFor(x => x.BirthDate).GreaterThanOrEqualTo(date);

            var model = new User() { BirthDate = new DateTime(1919, 1, 1) };

            Assert.IsFalse(validator.Validate(model).IsValid);
            Assert.AreEqual(validator.Validate(model).Errors[0].ErrorMessage, Messages.GreaterThanOrEqualTo("Birth Date", date.ToString()));

            model = new User() { BirthDate = date };

            Assert.IsTrue(validator.Validate(model).IsValid);
        }

        [Test]
        public void FieldIsLessThan()
        {
            var validator = new Validator<User>();
            var date = new DateTime(1920, 1, 1);
            validator.RuleFor(x => x.BirthDate).LessThan(date);

            var model = new User() { BirthDate = date };

            Assert.IsFalse(validator.Validate(model).IsValid);
            Assert.AreEqual(validator.Validate(model).Errors[0].ErrorMessage, Messages.LessThan("Birth Date", date.ToString()));

            model = new User() { BirthDate = new DateTime(1900, 1, 1) };

            Assert.IsTrue(validator.Validate(model).IsValid);
        }

        [Test]
        public void FieldIsLessThanOrEqual()
        {
            var validator = new Validator<User>();
            var date = new DateTime(1920, 1, 1);
            validator.RuleFor(x => x.BirthDate).LessThanOrEqualTo(date);

            var model = new User() { BirthDate = new DateTime(1921, 1, 1) };

            Assert.IsFalse(validator.Validate(model).IsValid);
            Assert.AreEqual(validator.Validate(model).Errors[0].ErrorMessage, Messages.LessThanOrEqualTo("Birth Date", date.ToString()));

            model = new User() { BirthDate = date };

            Assert.IsTrue(validator.Validate(model).IsValid);
        }

        [Test]
        public void FieldIsInclusiveBetween()
        {
            var validator = new Validator<User>();

            var startDate = new DateTime(1920, 1, 1);
            var endDate = DateTime.Now;

            validator.RuleFor(x => x.BirthDate).InclusiveBetween(startDate, endDate);

            var model = new User() { BirthDate = startDate.AddSeconds(-1) };

            Assert.IsFalse(validator.Validate(model).IsValid);
            Assert.AreEqual(validator.Validate(model).Errors[0].ErrorMessage, Messages.InclusiveBetween("Birth Date", startDate.ToString(), endDate.ToString(), startDate.AddSeconds(-1).ToString()));

            model = new User() { BirthDate = startDate };

            Assert.IsTrue(validator.Validate(model).IsValid);
        }

        [Test]
        [Explicit]
        public void FieldIsAnEntity()
        {
            var userValidator = new Validator<User>();
            var companyValidator = new Validator<Company>();

            var company = new Company();

            companyValidator.RuleFor(x => x.CompanyValue).NotEmpty();
            userValidator.RuleFor(x => x.Company).SetValidator(companyValidator);

            var model = new User() { Company = company };

            Assert.IsFalse(userValidator.Validate(model).IsValid);
            Assert.AreEqual(userValidator.Validate(model).Errors[0].ErrorMessage, Messages.NotEmpty("Company.Company Value"));

            model = new User() { Company = new Company() { CompanyValue = 20 } };

            Assert.IsTrue(userValidator.Validate(model).IsValid);
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
