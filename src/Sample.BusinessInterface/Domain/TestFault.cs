using System;
using System.Collections.Generic;
using System.Text;
using Simple.ServiceModel;

namespace Sample.BusinessInterface.Domain
{
    [SimpleFaultContract, Serializable]
    public class TestFault
    {
        public string Oi { get; set; }
    }
}
