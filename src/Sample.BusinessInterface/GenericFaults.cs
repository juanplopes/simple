using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using SimpleLibrary.DataAccess;

namespace Sample.BusinessInterface
{
    [DataContract]
    public enum GenericFaults
    {
        Test=10
    }
}
