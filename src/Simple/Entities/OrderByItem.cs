using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Expressions.Editable;
using System.Linq.Expressions;

namespace Simple.Entities
{
    [Serializable]
    public class OrderByItem<T>
    {
        public EditableExpression<Func<T, object>> Expression { get; set; }
        public bool Backwards { get; set; }

        public OrderByItem(EditableExpression<Func<T, object>> expr, bool backwards)
        {
            Expression = expr;
            Backwards = backwards;
        }

        public OrderByItem(EditableExpression<Func<T, object>> expr) : this(expr, false) { }

        public Expression<Func<T, object>> ToExpression()
        {
            return Expression.ToTypedLambda();
        }
    }
}
