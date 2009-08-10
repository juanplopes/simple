using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Simple.Expressions
{
    [Serializable]
    public class EditableConstantExpression : EditableExpression
    {
        public object Value { get; set;}
        public override ExpressionType NodeType
        {
            get { return ExpressionType.Constant; }
            set { }
        }

        public EditableConstantExpression()
        {
        }

        public EditableConstantExpression(object value)
        {
            Value = value;
        }

        public EditableConstantExpression(ConstantExpression startConstEx)
        {
            Value = startConstEx.Value;
        }

        public override Expression ToExpression()
        {
            return Expression.Constant(Value);
        }
    }
}
