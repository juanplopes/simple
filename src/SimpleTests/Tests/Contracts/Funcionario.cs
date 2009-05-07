using System;
using Simple.Filters;
using Simple.Rules;

namespace Simple.Tests.Contracts
{

    [Serializable]
    public partial class Funcionario : RuledEntity<Funcionario, IFuncionarioRules>
    {
        public Int32 Id { get; set; }
        public static PropertyName IdProperty =  "Id";
        public String Nome { get; set; }
        public static PropertyName NomeProperty = "Nome";
    }
}