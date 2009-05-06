using System;
using System.Runtime.Serialization;
using Simple.Reflection;
using Simple.Filters;

namespace Sample.BusinessInterface.Domain
{

    [Serializable]
    public partial class EmpresaFuncionario
    {
        public Int32 Id { get; set; }
        public static PropertyName IdProperty =  "Id";
        public Funcionario Funcionario { get; set; }
        public static PropertyName FuncionarioProperty = "Funcionario";

        public Empresa Empresa { get; set; }
        public static PropertyName EmpresaProperty = "Empresa";

    }
}