using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Config;
using System.Linq.Expressions;
using Simple.Expressions.Editable;
using Simple.Services;
using Simple.Expressions;

namespace Simple.Entities
{
    internal static class ExpressionExtensions
    {
        public static EditableExpression ToSerializable(this Expression expr)
        {
            return ToEditable(expr, true);
        }

        public static EditableExpression ToEditable(this Expression expr, bool funcletize)
        {
            return EditableExpression.CreateEditableExpression(expr, funcletize);
        }

    }
   
}
