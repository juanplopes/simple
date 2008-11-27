using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;
using SimpleLibrary.Filters;
using NHibernate.Mapping.Attributes;

namespace Sample.BusinessInterface.Domain
{
    [Serializable]
    [Class(Table = "empresa", Lazy = false)]
    public class Empresa
    {
        [Id(0, Column = "id_empresa", TypeType = typeof(int))]
        [Generator(1, Class = "native")]
        public virtual int Id { get; set; }
        public static PropertyName IdProperty = "Id";

        [Property(Column = "nome")]
        public virtual string Nome { get; set; }
        public static PropertyName NomeProperty = "Nome";
    }
}
