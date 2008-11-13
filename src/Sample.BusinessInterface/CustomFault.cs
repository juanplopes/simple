using System;
using System.Collections.Generic;

using System.Text;
using SimpleLibrary.DataAccess;
using SimpleLibrary.ServiceModel;
using System.Runtime.Serialization;

namespace Sample.BusinessInterface
{
    [DataContract, SimpleFaultContract]
    public class CustomFault : GenericFault<CustomFault.CustomFaultType>
    {
        public enum CustomFaultType
        {
            Test = 1,
            Test2 = 2
        }
        public CustomFault(CustomFaultType type) : base(type) { }
    }
}
