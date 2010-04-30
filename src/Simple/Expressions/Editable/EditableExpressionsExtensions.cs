using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Simple.Expressions.Editable
{
    public static partial class EditableExpressionsExtensions
    {
        public static EditableExpression<TDelegate> ToEditableExpression<TDelegate>(this Expression<TDelegate> ex)
        {
            return new EditableExpression<TDelegate>(ex);
        }

        public static LazyExpression<TDelegate> ToLazyExpression<TDelegate>(this Expression<TDelegate> ex)
        {
            return new LazyExpression<TDelegate>(ex);
        }

    }
}
