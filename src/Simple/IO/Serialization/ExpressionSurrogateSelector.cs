using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Linq.Expressions;
using System.Runtime.Remoting.Channels;
using Simple.Expressions.Editable;
using Simple.Expressions;

namespace Simple.IO.Serialization
{
    public class ExpressionSurrogateSelector : ISerializationSurrogate, ISurrogateSelector
    {

        #region ISurrogateSelector Members
        public ISurrogateSelector Next { get; protected set; }

        public void ChainSelector(ISurrogateSelector selector)
        {
            this.Next = selector;
        }

        public ISurrogateSelector GetNextSelector()
        {
            return Next;
        }

        public ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector)
        {
            if (typeof(Expression).IsAssignableFrom(type))
            {
                selector = this;
                return this;
            }
            else if (Next != null)
            {
                selector = Next;
                return Next.GetSurrogate(type, context, out selector);
            }
            else
            {
                selector = null;
                return null;
            }
        }

        #endregion

        #region ISerializationSurrogate Members

        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            info.AddValue("_", SimpleSerializer.Binary().Serialize(EditableExpression.Create(Funcletizer.PartialEval((Expression)obj))));
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var expr = ((EditableExpression)SimpleSerializer.Binary().Deserialize(info.GetValue("_", typeof(byte[])) as byte[]));
            if (expr == null) return null;
            return expr.ToExpression();
        }

        #endregion
    }
}
