using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Simple.Expressions.Editable;
using Simple.IO.Serialization;
using System.Runtime.Serialization;

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
