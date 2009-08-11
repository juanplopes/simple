using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Expressions;
using System.Linq.Expressions;

namespace Simple.Entities
{
    [Serializable]
    public class OrderBy
    {
        public EditableExpression Expression { get; set; }
        public bool Backwards { get; set; }

        public OrderBy(EditableExpression expr, bool backwards)
        {
            Expression = expr;
            Backwards = backwards;
        }

        public OrderBy(EditableExpression expr) : this(expr, false) { }

        public static OrderByCollection Asc<T, O>(Expression<Func<T, O>> expr)
        {
            return new OrderByCollection().Asc(expr);
        }

        public static OrderByCollection Desc<T, O>(Expression<Func<T, O>> expr)
        {
            return new OrderByCollection().Desc(expr);
        }

        public static OrderByCollection None { get { return new OrderByCollection(); } }

    }
}
