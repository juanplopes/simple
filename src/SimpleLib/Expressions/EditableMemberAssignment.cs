using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Linq.Expressions;

namespace Simple.Expressions
{
    [Serializable]
    public class EditableMemberAssignment : EditableMemberBinding
    {
        public EditableExpression Expression { get; set; }
        public override MemberBindingType BindingType
        {
            get { return MemberBindingType.Assignment; }
            set { }
        }

        // Ctors
        public EditableMemberAssignment()
        {
        }

        public EditableMemberAssignment(MemberAssignment member)
            : base(member.BindingType, member.Member)
        {
            Expression = EditableExpression.CreateEditableExpression(member.Expression);
        }

        // Methods
        public override MemberBinding ToMemberBinding()
        {
            return System.Linq.Expressions.Expression.Bind(Member, Expression.ToExpression());
        }

    }
}
