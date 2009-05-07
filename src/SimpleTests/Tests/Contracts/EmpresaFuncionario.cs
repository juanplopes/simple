using System;
using Simple.Rules;
using Simple.Filters;

namespace Simple.Tests.Contracts
{

    [Serializable]
    public partial class EmpresaFuncionario : RuledEntity<EmpresaFuncionario, IEmpresaFuncionarioRules>
    {
        public Int32 Id { get; set; }
        public static PropertyName IdProperty =  "Id";
        public Funcionario Funcionario { get; set; }
        public static PropertyName FuncionarioProperty = "Funcionario";

        public Empresa Empresa { get; set; }
        public static PropertyName EmpresaProperty = "Empresa";

    }
}