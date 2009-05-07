using System;
using Simple.Filters;
using Simple.Rules;
using NHibernate.Mapping.Attributes;

namespace Simple.Tests.Contracts
{

    [Serializable, Class(Lazy = false)]
    public partial class Funcionario : Entity<Funcionario, IFuncionarioRules>
    {
        [Id(0, Name = "Id")]
        [Generator(1, Class = "identity")]
        public virtual int Id { get; set; }
        public static PropertyName IdProperty = "Id";

        [Property]
        public virtual String Nome { get; set; }
        public static PropertyName NomeProperty = "Nome";
    }
}