using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Tests.Metadata
{
    public class Constants
    {
        //public const string ConnectionString = @"server=agdocdb; user id=agdoc;password=agdoc";
        //public const string MetadataProvider = "System.Data.OracleClient";

        public const string ConnectionString = @"server=.\sqlexpress; initial catalog=Simple; integrated security=sspi";
        public const string MetadataProvider = "System.Data.SqlClient";

        //public const string ConnectionString = @"server=localhost; database=schema_tests; user id=root";
        //public const string MetadataProvider = "MySql.Data.MySqlClient";


    }
}
