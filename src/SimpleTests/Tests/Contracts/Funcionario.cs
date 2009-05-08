using System;
using Simple.Filters;
using Simple.Rules;
using FluentNHibernate.Mapping;

namespace Simple.Tests.Contracts
{

    [Serializable]
    public partial class Funcionario : Entity<Funcionario, IFuncionarioRules>
    {
        public int Id { get; set; }
        public static PropertyName IdProperty = "Id";

        public String Nome { get; set; }
        public static PropertyName NomeProperty = "Nome";

        public class Map : ClassMap<Funcionario>
        {
            public Map()
            {
                Id(e => e.Id).GeneratedBy.Identity();
                Map(e => e.Nome);
            }
        }

    }
}