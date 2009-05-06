using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.IO
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class StringOffsetAttribute : Attribute
    {
        public int? Start { get; private set; }
        public int Length { get; private set; }

        public StringOffsetAttribute(int start, int length) : this(length)
        {
            Start = start;
        }

        public StringOffsetAttribute(int length)
        {
            Length = length;
        }
    }
}
