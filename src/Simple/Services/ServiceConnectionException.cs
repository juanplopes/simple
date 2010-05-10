using System;

namespace Simple.Services
{
    [global::System.Serializable]
    public class ServiceConnectionException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public ServiceConnectionException() { }
        public ServiceConnectionException(string message) : base(message) { }
        public ServiceConnectionException(string message, Exception inner) : base(message, inner) { }
        protected ServiceConnectionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
