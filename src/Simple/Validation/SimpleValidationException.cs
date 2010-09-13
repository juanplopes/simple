using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Simple.Validation
{
    [Serializable]
    public class SimpleValidationException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public ValidationList Errors { get; set; }

        public static string CreateExceptionMessage(ValidationList list)
        {
            var first = list.FirstOrDefault();
            int reimaing = list.Count - 1;
            if (first != null)
            {
                string more = reimaing > 0 ? string.Format(" ({0} more)", reimaing) : "";
                return string.Format("{0}{1}", first.Message, more);
            }
            else
            {
                return "Validation Exception";
            }
        }

        public SimpleValidationException() : this(new ValidationList()) { }

        public SimpleValidationException(string propertyName, string message) : this(new ValidationList() { new ValidationItem(propertyName, message) }) 
        { }
        
        public SimpleValidationException(ValidationList list)
            : base(CreateExceptionMessage(list))
        {
            Errors = list;
        }

        protected SimpleValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Errors = (ValidationList)info.GetValue("Errors", typeof(ValidationList));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Errors", Errors);
        }
    }
}
