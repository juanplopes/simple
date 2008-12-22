using System;
using System.Runtime.Serialization;
using BasicLibrary.Reflection;
using SimpleLibrary.Filters;

namespace Sample.BusinessInterface.Domain
{

    [Serializable]
    public partial class Empresa
    {
        public Int32 Id { get; set; }
        public static PropertyName IdProperty =  "Id";
        //public Byte[] Version { get; set; }
        //public static PropertyName VersionProperty = "Version";
        public String Nome { get; set; }
        public static PropertyName NomeProperty = "Nome";
    }
}