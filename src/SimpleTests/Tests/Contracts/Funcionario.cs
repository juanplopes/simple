using System;
using Simple.Filters;
using Simple.Rules;
using NHibernate.Mapping.Attributes;

namespace Simple.Tests.Contracts
{

    [Serializable, Class]
    public partial class Funcionario : Entity<Funcionario, IFuncionarioRules>
    {
        [Id(0, TypeType = typeof(Guid), UnsavedValueObject=null)]
        [Generator(1, Class = "guid")]
        public virtual Guid? Id { get; set; }
        public static PropertyName IdProperty =  "Id";

        [Property]
        public virtual String Nome { get; set; }
        public static PropertyName NomeProperty = "Nome";
    }
}