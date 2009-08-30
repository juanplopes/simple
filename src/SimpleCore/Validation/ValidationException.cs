using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Validator.Engine;

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

        protected InvalidValue[] _invalidValues;
        public IList<InvalidValue> InvalidValues
        {
            get { return _invalidValues; }
        }

        public ValidationException()
            : base()
        {
            _invalidValues = new InvalidValue[0];
        }

        public ValidationException(InvalidValue[] values)
            : base(CreateMessage(values))
        {
            _invalidValues = values;
        }

        public ValidationException(InvalidValue[] values, Exception inner)
            : base(CreateMessage(values), inner)
        {
            _invalidValues = values;
        }

        protected static string CreateMessage(InvalidValue[] values)
        {
            return values.Length > 0 ? values[0].PropertyName + " " + values[0].Message : "no message";
        }

        protected ValidationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
            _invalidValues = (InvalidValue[])info.GetValue("InvalidValues", typeof(InvalidValue[]));
        }

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            info.AddValue("InvalidValues", _invalidValues);
        }
    }

}
