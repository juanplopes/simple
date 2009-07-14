using System.Collections.Generic;
using SimpleLibrary.Rules;
using Sample.BusinessInterface.Rules;
using SimpleLibrary.Filters;
using SimpleLibrary.DataAccess;
using Sample.BusinessInterface.Domain;
using System.IO;
using System.Text.RegularExpressions;
using System;
using BasicLibrary.Logging;
using BasicLibrary.Common;

namespace Sample.UserInterface2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] campos = new string[] { "Nome", "Id" };
            Pair<string, bool>[] orderBy = new Pair<string, bool>[] { 
                new Pair<string, bool>("Id", true),
                new Pair<string, bool>("Nome", false)};

            Filter filter = BooleanExpression.True;
            foreach (string campo in campos)
            {
                filter = new AndExpression(filter, new LikeExpression(campo, "whatever"));
            }

            OrderByCollection col = new OrderByCollection();
            foreach (var pair in orderBy)
            {
                col.Add(new OrderBy(pair.First, pair.Second));
            }

            Console.ReadLine();
        }
    }
}

