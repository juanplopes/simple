using System;
using System.Xml.Serialization;
using Simple.Filters;
using Simple.Rules;
using NHibernate.Mapping.Attributes;

namespace Simple.Tests.Contracts
{

    [XmlRoot, Class(Lazy = false)]
    public partial class Empresa : Entity<Empresa, IEmpresaRules>
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