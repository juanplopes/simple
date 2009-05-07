using System;
using System.Xml.Serialization;
using Simple.Filters;
using Simple.Rules;

namespace Simple.Tests.Contracts
{

    [XmlRoot]
    public partial class Empresa : RuledEntity<Empresa, IEmpresaRules>
    {
        public Int32 Id { get; set; }
        public static PropertyName IdProperty =  "Id";
        public String Nome { get; set; }
        public static PropertyName NomeProperty = "Nome";
    }
}