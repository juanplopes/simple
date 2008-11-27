using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;
using SimpleLibrary.Filters;
using NHibernate.Mapping.Attributes;

namespace Sample.BusinessInterface.Domain
{
    [Serializable]
    [Class(Table = "empresa_funcionario")]
    public class EmpresaFuncionario
    {
        [Id(0, Column = "id_empresa_funcionario", TypeType = typeof(int))]
        [Generator(1, Class = "native")]
        public virtual int Id { get; set; }
        public static PropertyName IdProperty = "Id";

        [ManyToOne(Column="id_empresa", ClassType=typeof(Empresa))]
        public virtual Empresa Empresa { get; set; }
        public static PropertyName EmpresaProperty = "Empresa";

        [ManyToOne(Column="id_funcionario", ClassType=typeof(Funcionario))]
        public virtual Funcionario Funcionario { get; set; }
        public static PropertyName FuncionarioProperty = "Funcionario";
    }
}
