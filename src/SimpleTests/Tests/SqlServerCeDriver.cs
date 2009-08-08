using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NHibernate.SqlTypes;
using System.Data.SqlServerCe;

namespace Simple.Tests
{
    public class SqlServerCeDriver : NHibernate.Driver.SqlServerCeDriver
    {
        protected override void InitializeParameter(IDbDataParameter dbParam, string name, SqlType sqlType)
        {
            base.InitializeParameter(dbParam, name, sqlType);

            if (sqlType is BinarySqlType)
            {
                var parameter = (SqlCeParameter)dbParam;
                parameter.SqlDbType = SqlDbType.Image;
            }
        }

        public override IDbConnection CreateConnection()
        {
            return base.CreateConnection();
        }
    } 
}
