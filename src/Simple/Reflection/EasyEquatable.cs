using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Reflection
{
    [Serializable]
    public abstract class EasyEquatable
    {
        public abstract EqualityHelper CreateHelper();
        
        EqualityHelper _helper = null;
        private EqualityHelper GetHelper()
        {
            if (_helper == null)
                _helper = CreateHelper();

            return _helper;
        }


        public override bool Equals(object obj)
        {
            return GetHelper().ObjectEquals(this, obj);
        }

        public override int GetHashCode()
        {
            return GetHelper().ObjectGetHashCode(this);
        }

        public override string ToString()
        {
            return GetHelper().ObjectToString(this);
        }
    }
}
