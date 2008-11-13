using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;
using SimpleLibrary.Filters;

namespace Sample.BusinessInterface.Domain
{
    [DataContract(Name = "Funcionario", Namespace = "BusinessInterface.Domain")]
    public class Funcionario
    {
        [DataMember]
        public virtual int IdFuncionario { get; set; }
        public static PropertyName IdFuncionarioProperty = "IdFuncionario";

        [DataMember]
        public virtual string Nome { get; set; }
        public static PropertyName NomeProperty = "Nome";
    }
}
