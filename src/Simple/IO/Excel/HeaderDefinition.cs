using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Simple.Patterns;
using System.Reflection;
using System.Linq.Expressions;
using Simple.Reflection;

namespace Simple.IO.Excel
{
    public class HeaderDefinition<T> : List<IHeaderItem>
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

        public IHeaderItem Register(Expression<Func<T, object>> expr)
        {
            var item = new HeaderItem(expr.GetMemberList().ToSettable());
            Add(item);
            return item;
        }

        public void Skip(int count)
        {
            AddRange(Enumerable.Repeat<IHeaderItem>(new SkippingHeaderItem(), count));
        }
    }

}
