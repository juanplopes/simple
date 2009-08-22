using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Simple.Expressions.Editable
{
    [Serializable]
    public class EditableConditionalExpression : EditableExpression
    {
        public EditableExpression Test { get; set; }
        public EditableExpression IfTrue { get; set; }
        public EditableExpression IfFalse { get; set; }
        public override ExpressionType NodeType { get; set; }

        public EditableConditionalExpression()
        {
        }

        public EditableConditionalExpression(ConditionalExpression condEx)
        {
            NodeType = condEx.NodeType;
            Test = EditableExpression.CreateEditableExpression(condEx.Test);
            IfTrue = EditableExpression.CreateEditableExpression(condEx.IfTrue);
            IfFalse = EditableExpression.CreateEditableExpression(condEx.IfFalse);
        }

        public EditableConditionalExpression(ExpressionType nodeType, EditableExpression test, EditableExpression ifTrue, EditableExpression ifFalse)
        {
            NodeType = nodeType;
            Test = test;
            IfTrue = ifTrue;
            IfFalse = ifFalse;
        }

        // Methods
        public override Expression ToExpression()
        {
            return Expression.Condition(Test.ToExpression(), IfTrue.ToExpression(), IfFalse.ToExpression());
        }
    }
}
