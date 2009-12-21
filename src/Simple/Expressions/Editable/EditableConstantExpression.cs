using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Simple.Expressions.Editable
{
    [Serializable]
    public class EditableConstantExpression : EditableExpression
    {
        public object Value { get; set;}
        public Type ConstantType { get; set; }
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
            ConstantType = (value != null ? value.GetType() : typeof(void));
        }

        public EditableConstantExpression(ConstantExpression startConstEx)
        {
            Value = startConstEx.Value;
            ConstantType = startConstEx.Type;
        }

        public override Expression ToExpression()
        {
            return Expression.Constant(Value, ConstantType);
        }
    }
}
