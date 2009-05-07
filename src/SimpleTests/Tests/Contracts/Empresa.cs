using System;
using System.Xml.Serialization;
using Simple.Filters;
using Simple.Rules;
using NHibernate.Mapping.Attributes;

namespace Simple.Tests.Contracts
{

    [XmlRoot, Class]
    public partial class Empresa : Entity<Empresa, IEmpresaRules>
    {
        [Id(0, TypeType = typeof(Guid), UnsavedValueObject = null)]
        [Generator(1, Class="guid")]
        public virtual Guid? Id { get; set; }
        public static PropertyName IdProperty =  "Id";
        
        [Property]
        public virtual String Nome { get; set; }
        public static PropertyName NomeProperty = "Nome";
    }
}