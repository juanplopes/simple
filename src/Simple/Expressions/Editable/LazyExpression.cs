using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Simple.IO.Serialization;

namespace Simple.Expressions.Editable
{
    [Serializable]
    public class LazyExpression<TDelegate> : LazySerializer<Expression<TDelegate>, EditableExpression<TDelegate>>
    {
        public LazyExpression(Expression<TDelegate> real) : base(real) { }
        protected LazyExpression(SerializationInfo info, StreamingContext context) : base(info, context) { }

        protected override Expression<TDelegate> TransformToReal(EditableExpression<TDelegate> proxy)
        {
            return proxy.ToTypedLambda();
        }

        protected override EditableExpression<TDelegate> TransformToProxy(Expression<TDelegate> real)
        {
            return real.ToEditableExpression();
        }
    }
}
