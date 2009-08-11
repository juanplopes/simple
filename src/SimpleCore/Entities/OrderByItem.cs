using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Expressions;
using System.Linq.Expressions;

namespace Simple.Entities
{
    [Serializable]
    public class OrderByItem
    {
        public EditableExpression Expression { get; set; }
        public bool Backwards { get; set; }

        public OrderByItem(EditableExpression expr, bool backwards)
        {
            Expression = expr;
            Backwards = backwards;
        }

        public OrderByItem(EditableExpression expr) : this(expr, false) { }
    }
}
