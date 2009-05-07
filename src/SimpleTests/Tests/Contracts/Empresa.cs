using System;
using System.Xml.Serialization;
using Simple.Filters;
using Simple.Rules;
using FluentNHibernate.Mapping;

namespace Simple.Tests.Contracts
{

    [XmlRoot]
    public partial class Empresa : Entity<Empresa, IEmpresaRules>
    {
        public virtual int Id { get; set; }
        public static PropertyName IdProperty = "Id";
        public virtual String Nome { get; set; }
        public static PropertyName NomeProperty = "Nome";

        public class Map : ClassMap<Empresa>
        {
            public Map()
            {
                Id(e => e.Id).GeneratedBy.Identity();
                Map(e => e.Nome);
            }
        }
    }
}