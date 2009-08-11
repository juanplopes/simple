using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Simple.Expressions;

namespace Simple.Entities
{
    [Serializable]
    public class OrderByCollection : List<OrderBy>
    {
        public OrderByCollection Asc<T, O>(params Expression<Func<T, O>>[] exprs)
        {
            foreach (var expr in exprs)
                this.Add(new OrderBy(EditableExpression.CreateEditableExpression(expr), false));

            return this;
        }

        public OrderByCollection Desc<T, O>(params Expression<Func<T, O>>[] exprs)
        {
            foreach (var expr in exprs)
                this.Add(new OrderBy(EditableExpression.CreateEditableExpression(expr), true));

            return this;
        }
    }
}
