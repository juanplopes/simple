using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Simple.Common;
using System.Diagnostics;

namespace Simple.Web.Mvc
{
    public static class SelectListExtensions
    {
        public static IEnumerable<SelectListItem> Sort(this IEnumerable<SelectListItem> list)
        {
            return list.OrderBy(x => x.Text);
        }

        public static IEnumerable<SelectListItem> ToSelectList<T>(this IEnumerable<T> list, Func<T, object> idExpr, Func<T, object> textExpr, params object[] selectedValues)
        {
            if (list == null) return new SelectListItem[0];

            var selected = new HashSet<object>(selectedValues);

            return ToMultiSelectList(list, idExpr, textExpr, x => selected.Contains(idExpr(x)));
        }

        public static IEnumerable<SelectListItem> ToMultiSelectList<T>(this IEnumerable<T> list, Func<T, object> idExpr, Func<T, object> textExpr, Func<T, bool> isSelectedFunc)
        {
            if (list == null) yield break;

            foreach (var item in list)
            {
                yield return new SelectListItem()
                {
                    Text = string.Format("{0}", textExpr(item)),
                    Value = string.Format("{0}", idExpr(item)),
                    Selected = isSelectedFunc(item)
                };
            }
        }

        public static IEnumerable<SelectListItem> Select(this IEnumerable<SelectListItem> list, object item)
        {
            foreach (var listItem in list)
            {
                listItem.Selected = listItem.Value == string.Format("{0}", item);
                yield return listItem;
            }
        }

       
    }
}
