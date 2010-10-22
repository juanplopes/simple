using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Simple.Reflection;

namespace Simple.Expressions
{
    public class ExpressionWrapper<T, P> : IProperty
    {
        public Expression<Func<T, P>> Expression { get; protected set; }

        private CompositeSettableMember _settableMember = null;
        protected ISettableMemberInfo SettableMember
        {
            get
            {
                if (_settableMember == null)
                    _settableMember = new CompositeSettableMember(Expression.GetMemberList());

                return _settableMember;
            }
        }

        private Func<T, P> _compiledExpression = null;
        protected Func<T, P> CompiledExpression
        {
            get
            {
                if (_compiledExpression == null)
                    _compiledExpression = Expression.Compile();

                return _compiledExpression;
            }
        }


        public Type Type
        {
            get { return Expression.Type; }
        }

        public bool CanRead
        {
            get { return true; }
        }

        public bool CanWrite
        {
            get
            {
                try
                {
                    return SettableMember.CanWrite;
                }
                catch (NotSupportedException)
                {
                    return false;
                }
            }
        }

        public void Set(object target, object value, params object[] index)
        {
            SettableMember.Set(target, value, index);

        }

        public void Set(object target, object value)
        {
            Set(target, value, null);
        }

        public object Get(object target, params object[] index)
        {
            return CompiledExpression((T)target);
        }

        public object Get(object target)
        {
            return Get(target, null);
        }


    }
}
