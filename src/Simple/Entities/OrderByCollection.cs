using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Simple.Expressions.Editable;

namespace Simple.Entities
{
    [Serializable]
    public class OrderBy<T> : List<OrderByItem>
    {
        public OrderBy<T> Asc(Expression<Func<T, object>> expr)
        {
            this.Add(new OrderByItem(EditableExpression.Create(expr), false));
            return this;
        }

        public OrderBy<T> Desc(Expression<Func<T, object>> expr)
        {
            this.Add(new OrderByItem(EditableExpression.Create(expr), true));
            return this;
        }

        public OrderBy<T> None
        {
            get { return this; }
        }
    }
}
