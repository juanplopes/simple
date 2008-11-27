using System;
using System.Collections.Generic;
using System.Threading;
using Sample.BusinessInterface;
using Sample.BusinessInterface.Domain;
using SimpleLibrary.Filters;
using SimpleLibrary.Rules;
using BasicLibrary.Logging;
using BasicLibrary.Configuration;
using System.Globalization;

namespace Sample.UserInterface2
{



    class Program
    {
        delegate R SelectDelegate<T, R>(T entity);


        static R Transform<T, R>(T entity, SelectDelegate<T, R> del)
        {
            return del(entity);
        }

        static void Main(string[] args)
        {
            Empresa e = null;

            SelectDelegate<Empresa, string> del = new SelectDelegate<Empresa, string>(x => x.Nome);

            var s = Transform(e, x => x.Nome);
            

            MainLogger.Get<Program>().Debug("alguma mensagem");
            MainLogger.Get<Program>().Warn("warn message");
        }



    }
}

