using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Schema.Tests
{
    public class OracleTest : BaseDbTest
    {
        public override string ConnectionString
        {
            get { return "Data Source=agdocdb; User id=agdoc; Password=agdoc;"; }
        }

        public override string MigratorProvider
        {
            get { return "Oracle"; }
        }

        public override string MetadataProvider
        {
            get { return "System.Data.OracleClient"; }
        }

    }
}
