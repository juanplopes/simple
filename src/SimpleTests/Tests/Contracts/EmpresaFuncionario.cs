using System;
using Simple.Rules;
using Simple.Filters;
using NHibernate.Mapping.Attributes;

namespace Simple.Tests.Contracts
{

    [Serializable, Class(Lazy = false)]
    public partial class EmpresaFuncionario : Entity<EmpresaFuncionario, IEmpresaFuncionarioRules>
    {
        [Id(0, Name = "Id")]
        [Generator(1, Class = "identity")]
        public virtual int Id { get; set; }
        public static PropertyName IdProperty = "Id";

        [ManyToOne(Lazy = Laziness.False)]
        public virtual Funcionario Funcionario { get; set; }
        public static PropertyName FuncionarioProperty = "Funcionario";

        [ManyToOne(Lazy = Laziness.False)]
        public virtual Empresa Empresa { get; set; }
        public static PropertyName EmpresaProperty = "Empresa";

    }
}