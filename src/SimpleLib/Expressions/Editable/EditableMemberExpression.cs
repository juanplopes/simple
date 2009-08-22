using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Simple.Expressions.Editable
{
    [Serializable]
    public class EditableMemberExpression : EditableExpression
    {
        // Properties
        [IgnoreDataMember]
        public MemberInfo Member
        {
            get;
            set;
        }

        public EditableExpression Expression
        {
            get;
            set;
        }

        public string MemberName
        {
            get
            {
                return Member.ToSerializableForm();
            }
            set
            {
                Member = Member.FromSerializableForm(value);
            }
        }
        
        public override ExpressionType NodeType
        {
            get { return ExpressionType.MemberAccess; }
            set { }
        }

        // Ctors
        public EditableMemberExpression()
        {

        }

        public EditableMemberExpression(Expression rawEx, MemberInfo member)
            : this(EditableExpression.CreateEditableExpression(rawEx), member)
        { 
        }

        public EditableMemberExpression(EditableExpression editEx, MemberInfo member)
        {
            Member = member;
            Expression = editEx;
        }

        public EditableMemberExpression(MemberExpression membEx)
            : this(EditableExpression.CreateEditableExpression(membEx.Expression), membEx.Member)
        { 
        }

        // Methods
        public override Expression ToExpression()
        {
            Expression expression = null;
            if (Expression != null)
            {
                expression = Expression.ToExpression();
            }

            return System.Linq.Expressions.Expression.MakeMemberAccess(expression, Member);
        }
    }
}
