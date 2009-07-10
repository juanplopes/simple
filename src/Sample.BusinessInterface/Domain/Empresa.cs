using System;
using System.Runtime.Serialization;
using BasicLibrary.Reflection;
using SimpleLibrary.Filters;
using System.Xml.Serialization;
using Sample.BusinessInterface.Rules;
using SimpleLibrary.Rules;

namespace Sample.BusinessInterface.Domain
{

    [XmlRoot]
    public partial class Empresa : RuledEntity<Empresa, IEmpresaRules>
    {
        public Int32 Id { get; set; }
        public static PropertyName IdProperty =  "Id";
        //public Byte[] Version { get; set; }
        //public static PropertyName VersionProperty = "Version";
        public String Nome { get; set; }
        public static PropertyName NomeProperty = "Nome";
    }
}