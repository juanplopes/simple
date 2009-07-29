using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.ConfigSource;
using FluentNHibernate.Mapping;
using Simple.Services;

namespace Simple.Tests.Contracts
{
    [Serializable, DefaultConfig(typeof(DBEnsurer))]
    public partial class RelEmpresaFuncionario : Entity<RelEmpresaFuncionario, IRelEmpresaFuncionarioRules>
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual EmpresaFuncionario Relacao { get; set; }

        public class Map : ClassMap<RelEmpresaFuncionario>
        {
            public Map()
            {
                Not.LazyLoad();

                Id(x => x.Id).GeneratedBy.Identity();
                Map(x => x.Nome);
                HasOne(x => x.Relacao).SetAttribute("lazy", "false");
            }
        }
    }
}
