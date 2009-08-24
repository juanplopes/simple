using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Tests.SimpleLib.Sample
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple=true)]
    public class Attribute1Attribute : Attribute { }
    public class Attribute2Attribute : Attribute { }
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class Attribute3Attribute : Attribute { }

    [Attribute1]
    [Attribute1]
    [Attribute2]
    public class AttributeTest
    {
        [Attribute2]
        [Attribute3]
        [Attribute3]
        public int TestProp { get; set; }
    }
}
