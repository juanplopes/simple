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
        protected Func<T> instanceCreator = () => MethodCache.Do.CreateInstance<T>();
        public int SkipRows { get; set;}
        public int MaxNullRows { get; set; }
        public bool HeaderRow { get; set; }

        public HeaderDefinition()
        {
            SkipRows = 0;
            MaxNullRows = 10;
            HeaderRow = true;
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


        public HeaderDefinition<T> SkippingRows(int rows)
        {
            SkipRows = rows;
            return this;
        }

        public HeaderDefinition<T> WithMaxNullRows(int rows)
        {
            MaxNullRows = rows;
            return this;
        }

        public HeaderDefinition<T> HasHeaderRow(bool value)
        {
            HeaderRow = value;
            return this;
        }
    }

}
