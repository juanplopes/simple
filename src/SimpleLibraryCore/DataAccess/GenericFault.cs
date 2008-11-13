using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Diagnostics;

namespace SimpleLibrary.DataAccess
{
    [DataContract]
    public abstract class GenericFault<T> where T : struct
    {
        [DataMember]
        public T Reason { get; set; }
        [DataMember]
        public object Information { get; set; }

        public GenericFault() : this(default(T), null) { }
        public GenericFault(T reason) : this(reason, null) { }

        public GenericFault(T reason, object information)
        {
            this.Reason = reason;
            this.Information = information;
        }

        public override string ToString()
        {
            return
                Reason.ToString() +
                (Information != null ?
                (": " + Information.ToString()) : string.Empty);
        }

        [DebuggerHidden]
        public void Throw()
        {
            throw (Exception)Activator.CreateInstance(
             typeof(FaultException<>).MakeGenericType(this.GetType()), this, new FaultReason(this.ToString()));
        }
    }
}
