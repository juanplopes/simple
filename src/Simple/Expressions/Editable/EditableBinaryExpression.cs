using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Simple.Expressions.Editable
{
    [Serializable]
    public partial class EditableBinaryExpression : EditableExpressionImpl<BinaryExpression>
    {
        public EditableExpression Left { get; set;}
        public EditableExpression Right { get; set;}
        public override ExpressionType NodeType { get; set;}

        public EditableBinaryExpression()
        {
        }

        public EditableBinaryExpression(BinaryExpression binex) : base(binex.Type)
        {
            Left = EditableExpression.Create(binex.Left);
            Right = EditableExpression.Create(binex.Right);
            NodeType = binex.NodeType;
        }

        public override BinaryExpression ToTypedExpression()
        {
            return Expression.MakeBinary(NodeType, Left.ToExpression(), Right.ToExpression());
        }
    }
}
