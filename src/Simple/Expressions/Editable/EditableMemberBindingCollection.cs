using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Simple.Expressions.Editable
{
    [Serializable]
    public class EditableMemberBindingCollection : List<EditableMemberBinding>
    {
        public EditableMemberBindingCollection() : base() { }
        public EditableMemberBindingCollection(IEnumerable<EditableMemberBinding> source) : base(source) { }
        public EditableMemberBindingCollection(IEnumerable<MemberBinding> source) 
        {
            foreach (MemberBinding ex in source)
                this.Add(EditableMemberBinding.CreateEditableMemberBinding(ex));
        }

        public IEnumerable<MemberBinding> GetMemberBindings()
        {
            foreach (EditableMemberBinding editEx in this)
                yield return editEx.ToMemberBinding();
        }       
    }
}
