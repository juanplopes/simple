using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Entities;
using Simple.Validation;

namespace Simple.Web.Mvc
{
    public static class EntityValidationExtensions
    {
        public static T BindWith<T>(this T model, Func<T, bool> binder)
          where T : IEntity<T>
        {
            if (!binder(model))
                throw new SimpleValidationException(model.Validate());

            return model;
        }
    }
}
