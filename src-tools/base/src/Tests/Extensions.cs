using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Sample.Project.Tests
{
    public static class Extensions
    {
        public static T GetValue<T>(this IEnumerable<SelectListItem> list, Func<string, T> func)
        {
            return GetValue(list, 0, func);
        }

        public static T GetValue<T>(this IEnumerable<SelectListItem> list, int index, Func<string, T> func)
        {
            return list.Select(x => func(x.Value)).Skip(index).First();
        }
    }
}
