using System;
using Simple.Rules;
using Simple.Filters;
using NHibernate.Mapping.Attributes;

namespace Simple.Tests.Contracts
{

    [Serializable, Class]
    public partial class EmpresaFuncionario : Entity<EmpresaFuncionario, IEmpresaFuncionarioRules>
    {
        [Id(0, TypeType = typeof(Guid), UnsavedValueObject=null)]
        [Generator(1, Class = "guid")]
        public virtual Guid? Id { get; set; }
        public static PropertyName IdProperty =  "Id";
        
        [ManyToOne]
        public virtual Funcionario Funcionario { get; set; }
        public static PropertyName FuncionarioProperty = "Funcionario";

        [ManyToOne]
        public virtual Empresa Empresa { get; set; }
        public static PropertyName EmpresaProperty = "Empresa";

    }
}