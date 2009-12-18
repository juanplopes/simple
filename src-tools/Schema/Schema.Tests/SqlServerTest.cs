using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Schema.Tests
{
    public class SqlServerTest : BaseDbTest
    {
        public override string ConnectionString
        {
            get { return "Data Source=.\\sqlexpress; Initial Catalog=Schema_Tests; Integrated Security=SSPI"; }
        }

        public override string MigratorProvider
        {
            get { return "SqlServer"; }
        }

        public override string MetadataProvider
        {
            get { return "System.Data.SqlClient"; }
        }

    }
}
