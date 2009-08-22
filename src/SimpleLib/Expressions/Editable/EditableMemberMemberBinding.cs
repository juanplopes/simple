using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Linq.Expressions;

namespace Simple.Expressions.Editable
{
    [Serializable]
    public class EditableMemberMemberBinding : EditableMemberBinding
    {
        // Properties
        public EditableMemberBindingCollection Bindings 
        { 
            get; 
            set; 
        }

        public override MemberBindingType BindingType
        {
            get { return MemberBindingType.MemberBinding; }
            set { }
        }

        // Ctors
        public EditableMemberMemberBinding()
        {
        }

        public EditableMemberMemberBinding(MemberMemberBinding member) : base(member.BindingType, member.Member)
        {
            Bindings = new EditableMemberBindingCollection(member.Bindings);
        }

        // Methods
        public override MemberBinding ToMemberBinding()
        {
            return Expression.MemberBind(Member, Bindings.GetMemberBindings());
        }


    }
}
