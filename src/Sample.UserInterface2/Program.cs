using System.Collections.Generic;
using Simple.Rules;
using Sample.BusinessInterface.Rules;
using Simple.Filters;
using Simple.DataAccess;
using Sample.BusinessInterface.Domain;
using System.IO;
using System.Text.RegularExpressions;
using System;

namespace Sample.UserInterface2
{
    class Program
    {
        static void Main(string[] args)
        {
            Empresa e = Empresa.Load(1);

        }
    }
}

