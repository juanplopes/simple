using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;

namespace Sample.BusinessInterface.Domain
{
    [DataContract]
    public class TestDTO : Empresa
    {
        [DataMember]
        public int Cnt { get; set; }
    }
}
