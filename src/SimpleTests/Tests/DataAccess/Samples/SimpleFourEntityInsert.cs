using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Tests.Contracts;

namespace Simple.Tests.DataAccess.Samples
{
    public class SimpleFourEntityInsert
    {
        public Empresa Empresa { get; set; }
        public Funcionario Funcionario { get; set; }
        public EmpresaFuncionario Mapping { get; set; }
        public RelEmpresaFuncionario Mapping2 { get; set; }

        public SimpleFourEntityInsert()
        {
            Empresa = new Empresa()
            {
                Nome = "Empresa1"
            }.Save();

            Funcionario = new Funcionario()
            {
                Nome = "Funcionario1"
            }.Save();

            Mapping = new EmpresaFuncionario()
            {
                Empresa = Empresa,
                Funcionario = Funcionario
            }.Save();
            Mapping2 = new RelEmpresaFuncionario()
            {
                Nome = "Rel1",
                Relacao = Mapping
            }.Save();


        }
    }
}
