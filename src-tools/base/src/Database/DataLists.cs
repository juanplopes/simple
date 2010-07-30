using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator.Data;

namespace Sample.Project.Database
{
    public class DataLists
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
