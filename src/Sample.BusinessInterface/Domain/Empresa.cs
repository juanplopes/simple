using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using SimpleLibrary.Filters;

namespace Sample.BusinessInterface.Domain
{
    [DataContract(Name="Empresa", Namespace="BusinessInterface.Domain")]
    public class Empresa
    {
        [DataMember]
        public virtual int IdEmpresa { get; set; }
        public static PropertyName IdEmpresaProperty = "IdEmpresa";

        [DataMember]
        public virtual string Nome { get; set; }
        public static PropertyName NomeProperty = "Nome";

        [DataMember]
        public virtual byte[] Version { get; set; }
        public static PropertyName VersionProperty = "Version";
    }
}
