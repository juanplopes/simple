using System;
using System.Linq.Expressions;

namespace Simple.Expressions.Editable
{
    [Serializable]
    public partial class EditableMemberAssignment : EditableMemberBinding
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
            Expression = EditableExpression.Create(member.Expression);
        }

        // Methods
        public override MemberBinding ToMemberBinding()
        {
            return System.Linq.Expressions.Expression.Bind(Member, Expression.ToExpression());
        }

    }
}
