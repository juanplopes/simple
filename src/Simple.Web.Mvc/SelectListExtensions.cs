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
using MvcContrib.FluentHtml.Expressions;

namespace Simple.Web.Mvc
{
    public static class SelectListExtensions
    {
        public static IModelSelectList FindSelectList<T, P>(this IViewDataContainer view, Expression<Func<T, P>> member, string viewDataKey)
            where T : class
        {
            var list = view.ViewData[viewDataKey] as IModelSelectList;
            var viewData = view.ViewData;

            if (list == null)
                return new ModelSelectList<P>(new P[0], x => x, x => x);

            var model = SafeNullable.Get(() => member.Compile()((T)viewData.Model)).ObjectValue;
            if (model != null)
                list = list.Select((P)model);
            else
                list = FromModelState(member.GetMemberName(), list, viewData);

            return list;

        }

        public static IModelSelectList FindSelectList(this IViewDataContainer view, string memberName, string viewDataKey)
        {
            var list = view.ViewData[viewDataKey] as IModelSelectList;
            var viewData = view.ViewData;
            if (list == null)
                return new ModelSelectList<string>(new string[0], x => x, x => x);

            list = FromModelState(memberName, list, viewData);

            return list;
        }

        private static IModelSelectList FromModelState(string memberName, IModelSelectList list, ViewDataDictionary viewData)
        {
            ModelState modelState;
            viewData.ModelState.TryGetValue(memberName, out modelState);
            if (modelState != null && modelState.Value != null)
                list = list.SelectValue(modelState.Value.AttemptedValue);
            return list;
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

        public static Select AutoSelect(this IViewDataContainer html, string name)
        {
            return html.AutoSelect(name, name);
        }


        public static Select AutoSelect(this IViewDataContainer html, string name, string viewDataKey)
        {
            var list = html.FindSelectList(name, viewDataKey);
            if (list == null) throw new ArgumentException("viewDataKey must contain a ModelSelectList<T>");

            return html.Select(name).Options(list);
        }



    }
}
