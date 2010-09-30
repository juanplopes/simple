using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Simple.Reflection
{
    public class CompositeSettableMember : ISettableMemberInfo
    {
        protected static MethodCache Cache = new MethodCache();

        private IList<ISettableMemberInfo> members;
        public IEnumerable<ISettableMemberInfo> Members { get { return members; } }
        public CompositeSettableMember(IEnumerable<ISettableMemberInfo> members)
        {
            this.members = members.ToList();
            if (this.members.Count == 0) throw new InvalidOperationException("Couldn't find any properties on expression");
        }

        private ISettableMemberInfo Last()
        {
            return members[members.Count - 1];
        }

        private ISettableMemberInfo First()
        {
            return members[0];
        }

        private IEnumerable<ISettableMemberInfo> AllExceptLast()
        {
            return members.Take(members.Count - 1);
        }

        #region ISettableMemberInfo Members

        public MemberInfo Member
        {
            get { return Last().Member; }
        }

        public string Name
        {
            get { return members.Select(x => x.Name).JoinProperty(); }
        }

        public Type Type
        {
            get { return Last().Type; }
        }

        public Type DeclaringType
        {
            get { return First().DeclaringType; }
        }

        public bool CanRead
        {
            get { return members.All(x => x.CanRead); }
        }

        public bool CanWrite
        {
            get { return Last().CanWrite; }
        }

        public void Set(object target, object value, params object[] index)
        {
            foreach (var member in AllExceptLast())
            {
                object temp = member.Get(target);
                if (temp == null)
                {
                    temp = Cache.CreateInstance(member.Type);
                    member.Set(target, temp);
                    target = temp;
                }
            }
            Last().Set(target, value, index);
        }

        public void Set(object target, object value)
        {
            Set(target, value, null);
        }

        public object Get(object target, params object[] index)
        {
            foreach (var member in AllExceptLast())
            {
                target = member.Get(target);
            }
            return Last().Get(target, index);
        }

        public object Get(object target)
        {
            return Get(target, null);
        }

        #endregion
    }
}
