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
    public class EditableTypeBinaryExpression : EditableExpression
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
        public override Expression ToExpression()
        {
            return System.Linq.Expressions.Expression.TypeIs(Expression.ToExpression(), Type);
        }        
    }
}
