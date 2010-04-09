using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Entities;

namespace Simple.Validation
{
    [global::System.Serializable]
    public class ValidationException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        protected IList<ValidationItem> _items;
        public IList<ValidationItem> Items
        {
            get { return _items; }
        }

        public ValidationException()
            : base()
        {
            _items = new ValidationItem[0];
        }

        public ValidationException(IList<ValidationItem> values)
            : base(CreateMessage(values))
        {
            _items = values;
        }

        public ValidationException(IList<ValidationItem> values, Exception inner)
            : base(CreateMessage(values), inner)
        {
            _items = values;
        }

        protected static string CreateMessage(IList<ValidationItem> values)
        {
            return values.Count > 0 ? values[0].PropertyName + " " + values[0].Message : "no message";
        }

        protected ValidationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
            _items = (IList<ValidationItem>)info.GetValue("Items", typeof(IList<ValidationItem>));
        }

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Items", _items);
        }
    }

}
