using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;
using SimpleLibrary.Filters;
using NHibernate.Mapping.Attributes;

namespace Sample.BusinessInterface.Domain
{
    [Serializable]
    [Class(Table = "funcionario", Lazy = false)]
    public class Funcionario
    {
        [Id(0, Column = "id_funcionario", TypeType = typeof(int))]
        [Generator(1, Class = "native")]
        public virtual int Id { get; set; }
        public static PropertyName IdProperty = "Id";

        [Property(Column = "nome")]
        public virtual string Nome { get; set; }
        public static PropertyName NomeProperty = "Nome";
    }
}
