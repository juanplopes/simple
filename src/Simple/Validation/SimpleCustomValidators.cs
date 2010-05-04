using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using FluentValidation.Internal;
using Simple.Entities;

namespace Simple.Validation
{
    public static class SimpleCustomValidators
    {
        #region FluentValidation Simple Extensions
        public static IRuleBuilderOptions<T, TProp> MustBeUnique<T, TProp>(this IRuleBuilderInitial<T, TProp> builder)
            where T : class, IEntity<T>
        {
            var ruleBuilder = builder as RuleBuilder<T, TProp>;

            return builder.Must((T model, TProp prop) => model.CheckPropertiesUniqueness<T>(ruleBuilder.Model.Expression));
        }
        #endregion

    }
}
