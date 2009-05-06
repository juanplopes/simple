using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test
{
    public class Class1
    {
        public string value;

        public static explicit operator string(Class1 t)
        {
            return t.value;
        }

        public static implicit operator Class1(string t)
        {
            Class1 a = new Class1();
            a.value = t;
            return a;
        }


    }
}
