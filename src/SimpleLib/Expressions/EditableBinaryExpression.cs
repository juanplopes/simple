using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Simple.Expressions
{
    [Serializable]
    public class EditableBinaryExpression : EditableExpression
    {
        public EditableExpression Left { get; set;}
        public EditableExpression Right { get; set;}
        public override ExpressionType NodeType { get; set;}

        public EditableBinaryExpression()
        {
        }

        public EditableBinaryExpression(BinaryExpression binex) : base(binex.Type)
        {
            Left = EditableExpression.CreateEditableExpression(binex.Left);
            Right = EditableExpression.CreateEditableExpression(binex.Right);
            NodeType = binex.NodeType;
        }

        public override Expression ToExpression()
        {
            return Expression.MakeBinary(NodeType, Left.ToExpression(), Right.ToExpression());
        }        
    }
}
