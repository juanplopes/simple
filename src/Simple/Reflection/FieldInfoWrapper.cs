using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Simple.Reflection
{
    [Serializable]
    public class FieldInfoWrapper : ISettableMemberInfo
    {
        public FieldInfo InternalMember { get; protected set; }
        public MemberInfo Member { get { return InternalMember; } }

        public FieldInfoWrapper(FieldInfo field)
        {
            this.InternalMember = field;
        }

        public string Name
        {
            get { return InternalMember.Name; }
        }

        public void Set(object target, object value, params object[] index)
        {
            InternalMember.SetValue(target, value);
        }

        public void Set(object target, object value)
        {
            Set(target, value, null);
        }

        public object Get(object target, params object[] index)
        {
            return InternalMember.GetValue(target);
        }

        public object Get(object target)
        {
            return Get(target, null);
        }

        #region ISettableMemberInfo Members


        public Type Type
        {
            get { return InternalMember.FieldType; }
        }

        public Type DeclaringType
        {
            get { return InternalMember.DeclaringType; }
        }

        #endregion

        #region ISettableMemberInfo Members


        public bool CanRead
        {
            get { return true; }
        }

        public bool CanWrite
        {
            get { return true; }
        }

        #endregion
    }
}
