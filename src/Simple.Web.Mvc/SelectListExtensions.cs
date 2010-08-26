using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Simple.Common;
using System.Diagnostics;
using System.Linq.Expressions;
using MvcContrib.FluentHtml;
using MvcContrib.FluentHtml.Elements;

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
                var model = SafeNullable.Get(() => member.Compile()((T)viewData.Model)).ObjectValue;
                if (model != null)
                {
                    list = list.Select((P)model);
                }
                else
                {
                    ModelState modelState;
                    viewData.ModelState.TryGetValue(member.GetMemberName(), out modelState);
                    if (modelState != null && modelState.Value != null)
                        list = list.SelectValue(modelState.Value.AttemptedValue);
                }
                return list;
            }
            else
            {
                return new ModelSelectList<P>(new P[0], x => x, x => x);
            }

        }


        public static ModelSelectList<T> ToSelectList<T>(this IEnumerable<T> list, Func<T, object> valueSelector, Func<T, object> textSelector)
        {
            return new ModelSelectList<T>(list, valueSelector, textSelector);
        }

        public static Select<T> AutoSelect<T, P>(this IViewModelContainer<T> html, Expression<Func<T, P>> member)
            where T : class
        {
            return AutoSelect(html, member, member.GetMemberName());
        }

        public static Select<T> AutoSelect<T, P>(this IViewModelContainer<T> html, Expression<Func<T, P>> member, string viewDataKey)
          where T : class
        {
            var expr = Expression.Lambda<Func<T, object>>(Expression.Convert(member.Body, typeof(object)), member.Parameters);
            var list = html.FindSelectList(member, viewDataKey);
            if (list == null) throw new ArgumentException("viewDataKey must contain a ModelSelectList<T>");

            return html
                .Select(expr)
                .Options(list);
        }

    }
}
