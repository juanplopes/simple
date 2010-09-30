using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Simple.Patterns;
using System.Reflection;
using System.Linq.Expressions;
using Simple.Reflection;

namespace Simple.Web.Mvc.Excel
{
    public class HeaderDefinition<T> : List<HeaderItem>
    {
        public Func<T> instanceCreator = () => MethodCache.Do.CreateInstance<T>();
        public HeaderDefinition()
        {
        }

        public HeaderDefinition<T> CreateInstanceWith(Func<T> instanceCreator)
        {
            this.instanceCreator = instanceCreator;
            return this;
        }

        public T CreateInstance()
        {
            return instanceCreator();
        }

        public HeaderItem Register(Expression<Func<T, object>> expr, string name)
        {
            var item = new HeaderItem(expr.GetMemberList().ToSettable(), name);
            Add(item);
            return item;
        }
    }

}
