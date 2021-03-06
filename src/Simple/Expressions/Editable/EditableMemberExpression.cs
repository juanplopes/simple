﻿using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;

namespace Simple.Expressions.Editable
{
    [Serializable]
    public partial class EditableMemberExpression : EditableExpressionImpl<MemberExpression>
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
        public override MemberExpression ToTypedExpression()
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
