using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Migrator.Framework;
using Migrator.Fluent;
using System.Data;

namespace Sample.Project.Migrations
{
    [Migration(1)]
    public class MigrationPlaceHolder : FluentMigration
    {
        public override void Up()
        {
		}

		public override void Down()
        {
        }
    }

}


