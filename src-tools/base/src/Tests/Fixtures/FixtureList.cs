using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator.Data;

namespace Example.Project.Tests.Fixtures
{
    public class FixtureList
    {
        public static void All()
        {
        }

        public static void Test()
        {
        }

        public static void Development()
        {
        }

        #region Helper
        protected static void Do<T>()
            where T : IDataList
        {
            DataManager.Execute<T>();
        }
        #endregion
    }
}
