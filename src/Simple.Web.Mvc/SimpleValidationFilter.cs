using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Simple.Patterns;
using Simple.Validation;

namespace Simple.Web.Mvc
{
    public class SimpleValidationFilter : FilterAttribute, IExceptionFilter, IActionFilter
    {
        public string ViewName { get; set; }
        public bool Clear { get; set; }
        public bool HandleAnyException { get; set; }

        public SimpleValidationFilter() { }
        public SimpleValidationFilter(string view)
        {
            this.ViewName = view;
        }

        public void AddValidationErrors(ControllerBase controller, IList<Pair<string>> errors)
        {
            var groupedErrors = errors.GroupBy(x => x.First ?? string.Empty);
            foreach (var error in groupedErrors)
            {
                var state = controller.ViewData.ModelState[error.Key];

                if (state != null) state.Errors.Clear();

                foreach (var message in error)
                {
                    controller.ViewData.ModelState.AddModelError(
                        error.Key,
                        message.Second);
                }
            }
        }

        #region IExceptionFilter Members

        public void OnException(ExceptionContext filterContext)
        {
            var ex = filterContext.Exception;
            var controller = filterContext.Controller;
            if (controller == null) return;

            List<Pair<string>> errors = new List<Pair<string>>();
            if (Clear)
                controller.ViewData.ModelState.Where(x => x.Value.Errors.Any()).ToList().ForEach(x => x.Value.Errors.Clear());

            if (ex is SimpleValidationException)
            {
                var ex2 = ex as SimpleValidationException;
                errors.AddRange(ex2.Errors.Select(x => new Pair<string>(x.PropertyName, x.Message)));
                AddValidationErrors(controller, errors);

                filterContext.Result = GetResult(filterContext, controller);
                filterContext.ExceptionHandled = true;
            }
            else if (HandleAnyException)
            {
                controller.ViewData.ModelState.AddModelError(string.Empty, ex.Message);
                filterContext.Result = GetResult(filterContext, controller);
                filterContext.ExceptionHandled = true;
            }
        }


        private ViewResult GetResult(ControllerContext filterContext, ControllerBase controller)
        {
            TryPrepareFormIfNeeded(controller);

            var result = new ViewResult()
            {
                ViewName = this.ViewName ?? ((string)filterContext.RouteData.Values["action"]),
                ViewData = controller.ViewData,
                TempData = controller.TempData
            };
            return result;
        }

        private static void TryPrepareFormIfNeeded(ControllerBase controller)
        {
            try
            {
                if (controller is IFormController)
                    (controller as IFormController).PrepareForm(controller);
            }
            catch (Exception e)
            {
                controller.ViewData.ModelState.AddModelError(string.Empty, e.Message);
            }
        }

        #endregion


        #region IActionFilter Members

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var controller = filterContext.Controller as Controller;
            if (controller == null) return;

            if (!controller.ModelState.IsValid)
                filterContext.Result = GetResult(filterContext, controller);
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        #endregion
    }
}
