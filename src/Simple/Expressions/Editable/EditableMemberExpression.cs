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
            get
            {
                return ReflectionExtensions.FromMemberSerializableForm(MemberName);
            }
            set
            {
                MemberName = value.ToSerializableForm();
            }
        }

        public string MemberName { get; set; }

        public EditableExpression Expression
        {
            get;
            set;
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
            : this(EditableExpression.Create(rawEx), member)
        { 
        }

        public EditableMemberExpression(EditableExpression editEx, MemberInfo member)
        {
            Member = member;
            Expression = editEx;
        }

        public EditableMemberExpression(MemberExpression membEx)
            : this(EditableExpression.Create(membEx.Expression), membEx.Member)
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
