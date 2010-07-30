using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Simple.Common;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Simple.Web.Mvc
{
    public static class SelectListExtensions
    {
        public static ModelSelectList<P> FindSelectList<T, P>(this IViewDataContainer view, Expression<Func<T, P>> member, string viewDataKey)
        {
            var list = view.ViewData[viewDataKey] as ModelSelectList<P>;
            var viewData = view.ViewData;
            if (list != null)
            {
                var model = SafeNullable.Get(() => member.Compile()((T)viewData.Model));
                if (model != null)
                {
                    list = list.Select(model);
                }
                else
                {
                    ModelState modelState;
                    viewData.ModelState.TryGetValue(member.GetMemberName(), out modelState);
                    if (modelState != null && modelState.Value != null)
                        list = list.SelectValue(modelState.Value.AttemptedValue);
                }

            }
            return list;
        }


        public static ModelSelectList<T> ToSelectList<T>(this IEnumerable<T> list, Func<T, object> valueSelector, Func<T, object> textSelector)
        {
            return new ModelSelectList<T>(list, valueSelector, textSelector);
        }
       
    }
}
