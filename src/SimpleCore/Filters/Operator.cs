using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Simple.Filters
{
    [Serializable]
    public abstract class Operator : Expression
    {
        [DataMember]
        protected Filter[] _filters;
        protected Operator(params Filter[] filters)
        {
            _filters = filters;
        }
    }
}
