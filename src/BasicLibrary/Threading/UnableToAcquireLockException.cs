using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Simple.Threading
{
    public class UnableToAcquireLockException : Exception
    {
        public string Type { get; set; }
        public int Id { get; set; }

        public UnableToAcquireLockException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public UnableToAcquireLockException() : base() { }
        public UnableToAcquireLockException(string type, int id)
            : this()
        {
            this.Type = type;
            this.Id = id;
        }
    }
}
