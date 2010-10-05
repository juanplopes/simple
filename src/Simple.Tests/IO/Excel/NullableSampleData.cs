using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpTestsEx;

namespace Simple.Tests.IO.Excel
{
    public class NullableSampleData
    {
        public enum Status
        {
            Ativo,
            Inativo,
            Cancelado
        }
        public string ColunaA { get; set; }
        public int? ColunaB { get; set; }
        public DateTime? ColunaC { get; set; }
        public Status? ColunaD { get; set; }
        public bool? ColunaE { get; set; }

        public NullableSampleData() { }

        public void AssertWith(string a, int? b, DateTime? c, Status? d, bool? e)
        {
            this.ColunaA.Should().Be(a);
            this.ColunaB.Should().Be(b);
            this.ColunaC.Should().Be(c);
            this.ColunaD.Should().Be(d);
            this.ColunaE.Should().Be(e);
        }
    }
}
