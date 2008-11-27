using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;
using SimpleLibrary.Filters;
using NHibernate.Mapping.Attributes;

namespace Sample.BusinessInterface.Domain
{
    [Serializable]
    public class EmpresaFuncionario
    {
        public virtual int Id { get; set; }
        public static PropertyName IdProperty = "Id";

        public virtual Empresa Empresa { get; set; }
        public static PropertyName EmpresaProperty = "Empresa";

        public virtual Funcionario Funcionario { get; set; }
        public static PropertyName FuncionarioProperty = "Funcionario";
    }
}
