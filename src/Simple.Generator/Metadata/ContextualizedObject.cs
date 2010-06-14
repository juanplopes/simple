using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Reflection;

namespace Simple.Metadata
{
    public abstract class ContextualizedObject : EasyEquatable
    {
        protected MetaContext Context { get; private set; }
        public ContextualizedObject(MetaContext context)
        {
            this.Context = context;
        }
    }
}
