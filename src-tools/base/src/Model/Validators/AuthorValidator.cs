using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Example.Project.Domain;
using FluentValidation;
using Simple;

namespace Example.Project.Validators
{
    public class AuthorValidator : AbstractValidator<Author>
    {
        public AuthorValidator()
        {
        }
    }
}
