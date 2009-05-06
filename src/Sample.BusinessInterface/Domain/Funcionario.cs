using System;
using System.Runtime.Serialization;
using Simple.Reflection;
using Simple.Filters;

namespace Sample.BusinessInterface.Domain
{

    [Serializable]
    public partial class Funcionario
    {
        public Int32 Id { get; set; }
        public static PropertyName IdProperty =  "Id";
        public String Nome { get; set; }
        public static PropertyName NomeProperty = "Nome";
    }
}