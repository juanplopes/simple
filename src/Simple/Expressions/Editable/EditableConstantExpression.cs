using System;
using System.Linq.Expressions;

namespace Simple.Expressions.Editable
{
    [Serializable]
    public partial class EditableConstantExpression : EditableExpressionImpl<ConstantExpression>
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

        public override ConstantExpression ToTypedExpression()
        {
            return Expression.Constant(Value, ConstantType);
        }
    }
}
