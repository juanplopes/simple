using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Simple.Patterns;
using System.Reflection;
using System.Linq.Expressions;

namespace Simple.Web.Mvc.Excel
{
    public class HeaderDefinition<T> : HeaderDefinition
    {
        public void Register(Expression<Func<T, object>> expr, string name)
        {
            Register(expr.GetMemberName().GetProperty(typeof(T)), name);
        }
    }

    public class HeaderDefinition : List<Pair<PropertyInfo, string>>
    {
        public void Register(PropertyInfo property, string name)
        {
            this.Add(Tuples.Get(property, name));
        }
    }
}
