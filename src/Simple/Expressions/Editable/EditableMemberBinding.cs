using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using System.Linq.Expressions;
using System.Xml.Serialization;

namespace Simple.Expressions.Editable
{
    [Serializable]
    public abstract class EditableMemberBinding
    {       
        // Properties
        public abstract MemberBindingType BindingType { get; set;}

        [IgnoreDataMember]
        public MemberInfo Member {get;set;}

        public string MemberName
        {
            get { return Member.ToSerializableForm(); }
            set { Member = Member.FromSerializableForm(value); }
        }

        // Ctors
        protected EditableMemberBinding()
        {
        }

        protected EditableMemberBinding(MemberBindingType type, MemberInfo member)
        {
            BindingType = type;
            Member = member;
        }

        // Methods
        public abstract MemberBinding ToMemberBinding();

        public static EditableMemberBinding CreateEditableMemberBinding(MemberBinding member)
        {
            if (member is MemberAssignment) return new EditableMemberAssignment(member as MemberAssignment);
            else if (member is MemberListBinding) return new EditableMemberListBinding(member as MemberListBinding);
            else if (member is MemberMemberBinding) return new EditableMemberMemberBinding(member as MemberMemberBinding);
            else return null;

        }
    }
}
