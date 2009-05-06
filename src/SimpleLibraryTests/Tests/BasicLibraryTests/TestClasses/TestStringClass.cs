using System;
using System.Collections.Generic;
using System.Text;
using Simple.IO;

namespace Simple.Tests.Lib.TestClasses
{
    public class TestStringClass1
    {
        [StringOffset(5), Formatter(typeof(NumericFormatter), "00000", 0)]
        public int TestInt { get; set; }
        [StringOffset(6), Formatter(typeof(NumericFormatter), "000000", 2)]
        public decimal TestDecimal { get; set; }

        [StringOffset(6), Formatter(typeof(NumericFormatter), "000000", 0)]
        public int TestInt2 { get; set; }
        [StringOffset(5), Formatter(typeof(NumericFormatter), "00000", 0)]
        public decimal TestDecimal2 { get; set; }

    }

    [Culture("pt-br")]
    public class TestStringClass2
    {
        [StringOffset(0, 5)]
        public int TestInt { get; set; }
        [StringOffset(5, 6)]
        public decimal TestDecimal { get; set; }

        [StringOffset(6)]
        public int TestInt2 { get; set; }

        [StringOffset(5), Culture("en-us")]
        public decimal TestDecimal2 { get; set; }
    }

    [Culture("pt-br")]
    public class TestStringClass3
    {
        [StringOffset(0, 3)]
        public int TestInt { get; set; }
        [StringOffset(0, 5)]
        public decimal TestDecimal { get; set; }

        [StringOffset(0, 3)]
        public int TestInt2 { get; set; }

        [StringOffset(0, 10), Culture("en-us")]
        public decimal TestDecimal2 { get; set; }
    }


    [Culture("pt-br")]
    public class TestStringClass4
    {
        [StringOffset(0, 5), Formatter(typeof(NumericFormatter), "00000", 0)]
        public int TestInt { get; set; }

        [StringOffset(8)]
        [Formatter(typeof(DateFormatter), "yyyyddMM")]
        public DateTime TestDateTime { get; set; }

    }

}
