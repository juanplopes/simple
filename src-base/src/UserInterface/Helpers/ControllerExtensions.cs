using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Simple;
using Simple.Entities;
using NHibernate.Validator.Engine;

namespace Sample.Project.Helpers
{
    public static class ControllerExtensions
    {
        public static bool Validate<T>(this Controller controller, T entity)
        {
            var svc = Simply.Do.Resolve<IEntityService<T>>();
            var errors = svc.Validate(entity);

            AddValidationErrors(controller, errors);
            return errors.Count == 0;
        }

        public static void AddValidationErrors(this Controller controller, IList<InvalidValue> values)
        {
            foreach (var error in values)
            {
                controller.ModelState.AddModelError(error.PropertyName, error.PropertyName + " " + error.Message);
            }
        }
    }
}
