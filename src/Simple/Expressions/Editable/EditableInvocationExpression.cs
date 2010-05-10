using System;
using System.Linq.Expressions;

namespace Simple.Expressions.Editable
{
    [Serializable]
    public partial class EditableInvocationExpression : EditableExpressionImpl<InvocationExpression>
    {
        public EditableExpression Expression { get; set;}
        public EditableExpressionCollection Arguments {get; set;}

        public override ExpressionType NodeType
        {
            get { return ExpressionType.Invoke; }
            set { }
        }

        // Ctors
        public EditableInvocationExpression()
        {
            Arguments = new EditableExpressionCollection();
        }

        public EditableInvocationExpression(InvocationExpression invocEx)
            : this()
        {
            Expression = EditableExpression.Create(invocEx.Expression);
            foreach (Expression ex in invocEx.Arguments)
                Arguments.Add(EditableExpression.Create(ex));
        }

        // Methods
        public override InvocationExpression ToTypedExpression()
        {
            return System.Linq.Expressions.Expression.Invoke(Expression.ToExpression(), Arguments.GetExpressions());
        }
    }
}
