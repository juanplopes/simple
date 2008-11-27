using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;
using SimpleLibrary.Filters;
using NHibernate.Mapping.Attributes;

namespace Sample.BusinessInterface.Domain
{
    [Serializable]
    public class Empresa
    {
        public virtual int Id { get; set; }
        public static PropertyName IdProperty = "Id";

        public virtual string Nome { get; set; }
        public static PropertyName NomeProperty = "Nome";
    }
}
