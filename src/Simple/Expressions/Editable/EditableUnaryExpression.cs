using System;
using System.Linq.Expressions;

namespace Simple.Expressions.Editable
{
    [Serializable]
    public partial class EditableUnaryExpression : EditableExpressionImpl<UnaryExpression>
    {
        // Properties
        public EditableExpression Operand
        {
            get;
            set;
        }
        
        public override ExpressionType NodeType
        {
            get;
            set;
        }

        // Ctors
        public EditableUnaryExpression()
        {
        }

        public EditableUnaryExpression(ExpressionType nodeType, EditableExpression operand, Type type)
        {
            NodeType = nodeType;
            Operand = operand;
            Type = type;
        }

        public EditableUnaryExpression(UnaryExpression unEx)
            : this(unEx.NodeType, EditableExpression.Create(unEx.Operand), unEx.Type)
        {
        }

        // Methods
        public override UnaryExpression ToTypedExpression()
        {
            if (NodeType == ExpressionType.UnaryPlus)
                return Expression.UnaryPlus(Operand.ToExpression());
            else if (NodeType == ExpressionType.Quote)
                return Expression.Quote(Operand.ToExpression());
            else
                return Expression.MakeUnary(NodeType, Operand.ToExpression(), Type);
        }
    }
}
