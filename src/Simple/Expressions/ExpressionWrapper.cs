using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Simple.Reflection;
using Simple.Expressions.Editable;

namespace Simple.Expressions
{
    [Serializable]
    public class ExpressionWrapper<T, P> : IProperty
    {
        public Expression<Func<T, P>> Expression { get { return PrivateExpression.Real; } }
        protected LazyExpression<Func<T, P>> PrivateExpression { get; set; }

        public string Name { get { return Expression.Body.ToString(); } }

        public ExpressionWrapper() { }
        public ExpressionWrapper(Expression<Func<T, P>> expression)
        {
            PrivateExpression = expression.ToLazyExpression();
        }

        private ISettableMemberInfo _settableMember = null;
        protected ISettableMemberInfo SettableMember
        {
            get
            {
                if (_settableMember == null)
                {
                    try
                    {
                        _settableMember = new CompositeSettableMember(PrivateExpression.Real.GetMemberList());
                    }
                    catch (NotSupportedException) { _settableMember = new EmptySettableMember(); }
                    catch (InvalidOperationException) { _settableMember = new EmptySettableMember(); }
                }

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
            get { return Expression.Body.Type; }
        }

        public bool CanRead
        {
            get { return true; }
        }

        public bool CanWrite
        {
            get
            {

                return SettableMember.CanWrite;
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
