using System;
using System.Linq.Expressions;

namespace Simple.Expressions.Editable
{
    [Serializable]
    public partial class EditableTypeBinaryExpression : EditableExpressionImpl<TypeBinaryExpression>
    {
        // Properties
        public EditableExpression Expression
        {
            get;
            set;
        }
        
        public override ExpressionType NodeType
        {
            get { return ExpressionType.TypeIs; }
            set { }
        }

        //Ctors
        public EditableTypeBinaryExpression()
        {
        }

        public EditableTypeBinaryExpression(TypeBinaryExpression typeBinEx) 
            : this(EditableExpression.Create(typeBinEx.Expression), typeBinEx.TypeOperand)
        { }

        public EditableTypeBinaryExpression(EditableExpression expression, Type type)
            : base(type)
        {
            Expression = expression;
        }

        // Methods
        public override TypeBinaryExpression ToTypedExpression()
        {
            return System.Linq.Expressions.Expression.TypeIs(Expression.ToExpression(), Type);
        }        
    }
}
