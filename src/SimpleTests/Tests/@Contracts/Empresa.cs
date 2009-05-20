using System;
using System.Xml.Serialization;
using Simple.Filters;
using Simple.Rules;
using FluentNHibernate.Mapping;

namespace Simple.Tests.Contracts
{

    [Serializable]
    public partial class Empresa : Entity<Empresa, IEmpresaRules>
    {
        public virtual int Id { get; set; }
        public static PropertyName IdProperty = "Id";
        public virtual String Nome { get; set; }
        public static PropertyName NomeProperty = "Nome";
    }

    public class EmpresaMap : ClassMap<Empresa>
    {
        public EmpresaMap()
        {
            Not.LazyLoad();

            Id(e => e.Id).GeneratedBy.Identity();
            Map(e => e.Nome);
        }
    }
}