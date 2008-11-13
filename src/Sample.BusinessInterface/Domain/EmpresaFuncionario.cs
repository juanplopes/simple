using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;
using SimpleLibrary.Filters;

namespace Sample.BusinessInterface.Domain
{
    [DataContract(Name = "EmpresaFuncionario", Namespace = "BusinessInterface.Domain")]
    public class EmpresaFuncionario
    {
        [DataMember]
        public virtual int IdEmpresaFuncionario { get; set; }
        public static PropertyName IdEmpresaFuncionarioProperty = "IdEmpresaFuncionario";

        [DataMember]
        public virtual Empresa Empresa { get; set; }
        public static PropertyName EmpresaProperty = "Empresa";

        [DataMember]
        public virtual Funcionario Funcionario { get; set; }
        public static PropertyName FuncionarioProperty = "Funcionario";
    }
}
