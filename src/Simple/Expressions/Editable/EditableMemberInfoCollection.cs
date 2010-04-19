using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Simple.Expressions.Editable
{
    [Serializable]
    public partial class EditableMemberInfoCollection : List<string>
    {
        public EditableMemberInfoCollection() : base() { }
        public EditableMemberInfoCollection(IEnumerable<string> source) : base(source) { }
        public EditableMemberInfoCollection(IEnumerable<MemberInfo> source) 
        {
            if (source != null)
            {
                foreach (MemberInfo ex in source)
                    this.Add(ex.ToSerializableForm());
            }
        }

        public IEnumerable<MemberInfo> GetMembers()
        {
            foreach (string editEx in this)
                yield return ReflectionExtensions.FromMemberSerializableForm(editEx);
        }   
    }
}
