using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Reflection
{
    [Serializable]
    public class EmptySettableMember : ISettableMemberInfo
    {
        public System.Reflection.MemberInfo Member
        {
            get { throw new NotSupportedException(); }
        }

        public string Name
        {
            get { throw new NotSupportedException(); }
        }

        public Type DeclaringType
        {
            get { throw new NotSupportedException(); }
        }

        public Type Type
        {
            get { throw new NotSupportedException(); }
        }

        public bool CanRead
        {
            get { return false; }
        }

        public bool CanWrite
        {
            get { return false; }
        }

        public void Set(object target, object value, params object[] index)
        {
            throw new NotSupportedException();
        }

        public void Set(object target, object value)
        {
            throw new NotSupportedException();
        }

        public object Get(object target, params object[] index)
        {
            throw new NotSupportedException();
        }

        public object Get(object target)
        {
            throw new NotSupportedException();
        }
    }
}
