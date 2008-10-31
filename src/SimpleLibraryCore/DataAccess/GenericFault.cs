using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace SimpleLibrary.DataAccess
{
    [DataContract]
    public class GenericFault
    {
        [DataMember]
        public int Reason { get; set; }
        [DataMember]
        public object Information { get; set; }


        public GenericFault() : this(0, null) { }
        public GenericFault(int reason) : this(reason, null) { }

        public GenericFault(int reason, object information)
        {
            this.Reason = reason;
            this.Information = information;
        }

        public T GetReason<T>() where T:struct
        {
            return (T)Enum.ToObject(typeof(T), Reason); 
        }

        public void Throw<T>()
        {
            Throw(typeof(T));
        }

        public void Throw(Type enumType)
        {
            string reason = ((Enum.GetName(enumType, Reason))).ToString() + ": " +(Information ?? string.Empty).ToString();
            throw (Exception)Activator.CreateInstance(
             typeof(FaultException<>).MakeGenericType(this.GetType()), this, new FaultReason(reason));
        }
    }
}
