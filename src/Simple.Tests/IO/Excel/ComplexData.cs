using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpTestsEx;

namespace Simple.Tests.IO.Excel
{
    public class ComplexData
    {
        public enum Status
        {
            Ativo,
            Inativo,
            Cancelado
        }

        public class ChildA
        {
            public string ColunaA { get; set; }
            public int ColunaB { get; set; }
        }

        public class ChildB
        {
            public ChildC PropC { get; set; }
        }

        public class ChildC
        {
            public DateTime ColunaC { get; set; }
            public Status ColunaD { get; set; }
            public bool ColunaE { get; set; }
        }

        public ChildA PropA { get; set; }
        public ChildB PropB { get; set; }

        public void AssertWith(string a, int b, DateTime c, Status d, bool e)
        {
            this.PropA.ColunaA.Should().Be(a);
            this.PropA.ColunaB.Should().Be(b);
            this.PropB.PropC.ColunaC.Should().Be(c);
            this.PropB.PropC.ColunaD.Should().Be(d);
            this.PropB.PropC.ColunaE.Should().Be(e);
        }
    }
}
