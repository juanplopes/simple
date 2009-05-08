using System;
using Simple.Rules;
using Simple.Filters;
using FluentNHibernate.Mapping;

namespace Simple.Tests.Contracts
{

    [Serializable]
    public partial class EmpresaFuncionario : Entity<EmpresaFuncionario, IEmpresaFuncionarioRules>
    {
        public int Id { get; set; }
        public static PropertyName IdProperty = "Id";

        public Funcionario Funcionario { get; set; }
        public static PropertyName FuncionarioProperty = "Funcionario";

        public Empresa Empresa { get; set; }
        public static PropertyName EmpresaProperty = "Empresa";

        public class Map : ClassMap<EmpresaFuncionario>
        {
            public Map()
            {
                Id(e => e.Id).GeneratedBy.Identity();
                HasOne(e => e.Funcionario);
                HasOne(e => e.Empresa);
            }
        }

    }
}