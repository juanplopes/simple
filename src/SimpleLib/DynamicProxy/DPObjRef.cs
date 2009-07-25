using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting;

namespace Simple.DynamicProxy
{
    public class DPRemotingTypeInfo : IRemotingTypeInfo
    {
        public DynamicProxyImpl DynamicProxy { get; set; }

        public DPRemotingTypeInfo(DynamicProxyImpl dp)
        {
            DynamicProxy = dp;
        }

        #region IRemotingTypeInfo Members

        public bool CanCastTo(Type fromType, object o)
        {
            return true;
        }

        public string TypeName
        {
            get
            {
                return typeof(DynamicProxyImpl).Name;
            }
            set
            {
            }
        }

        #endregion
    }

    public class DPObjRef : ObjRef
    {
        public DynamicProxyImpl DynamicProxy { get; set; }

        public DPObjRef(DynamicProxyImpl dp)
        {
            DynamicProxy = dp;
        }

        public override IRemotingTypeInfo TypeInfo
        {
            get
            {
                return new DPRemotingTypeInfo(DynamicProxy);
            }
            set
            {
            }
        }
    }
}
