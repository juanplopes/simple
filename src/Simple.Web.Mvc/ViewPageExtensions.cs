using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Simple.Web.Mvc
{
    public static class ViewPageExtensions
    {
        public static T RouteParam<T>(this ViewPage page, string name)
        {
            object obj = page.ViewContext.RouteData.Values[name];

            if (obj != null)
                return (T)Convert.ChangeType(obj, typeof(T));
            else
                return default(T);
        }
    }
}
