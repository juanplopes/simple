using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Simple.IO
{
    public class StringStream : MemoryStream
    {
        Encoding encoding = Encoding.UTF8;
        public Encoding Encoding { get { return encoding; } }

        public StringStream() : this(Encoding.UTF8) { }
        public StringStream(Encoding encoding)
        {
            this.encoding = encoding;
        }

        public StringStream(string initialValue) : this(initialValue, Encoding.UTF8) { }
        public StringStream(string initialValue, Encoding encoding)
            : base(encoding.GetBytes(initialValue))
        {
            this.encoding = encoding;
        }

        public string GetString()
        {
            return encoding.GetString(this.ToArray());
        }
    }
}
