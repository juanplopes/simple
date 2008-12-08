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
using SimpleLibrary.Config;
using SimpleLibrary.DataAccess;
using System.Web.UI;
using System.Collections;
using BasicLibrary.Common;
using System.Text.RegularExpressions;

namespace Sample.UserInterface2
{
    public class TestBusinessDaysProvider : IBusinessDaysProvider
    {
        public bool IsBusinessDay(DateTime pobjDate)
        {
            return pobjDate.Day % 2 == 0;
        }
    }

    public class Test2 : IBusinessDaysProvider
    {

        #region IBusinessDaysProvider Members

        public bool IsBusinessDay(DateTime pobjDate)
        {
            return pobjDate.Day % 2 != 0;
        }

        #endregion
    }

    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(4000);
            IEmpresaRules rules = RulesFactory.Create<IEmpresaRules>();

            for (int i = 0; i < 1000; i++)
            {
                Empresa e = new Empresa()
                {
                    Nome = "qualquer" + i
                };

                rules.Persist(e);
            }
            IList<Empresa> list = rules.ListAll(OrderBy.None());
        }
    }
}

