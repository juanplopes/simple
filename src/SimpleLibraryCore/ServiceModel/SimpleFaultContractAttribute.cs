using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleLibrary.ServiceModel
{
    [AttributeUsage(AttributeTargets.Class, Inherited=false)]
    public class SimpleFaultContractAttribute : Attribute
    {

    }
}
