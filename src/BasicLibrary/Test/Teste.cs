using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicLibrary.Configuration;
using BasicLibrary.Collections;

namespace Test
{
    [DefaultFile("teste.config")]
    public class Teste : ConfigRoot<Teste>
    {
        public static implicit operator int(Teste t)
        {
            return 2;
        }

        public static implicit operator string(Teste t)
        {
            return "oi";
        }
    }
}
