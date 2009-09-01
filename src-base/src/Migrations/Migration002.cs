using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Migrator.Framework;
using System.Data;

namespace Conspirarte.Migrations
{
    [Migration(2)]
    public class Migration002 : Migration
    {
        public override void Down()
        {
            Database.RemoveColumn("Users", "Id");
        }

        public override void Up()
        {
            Database.AddColumn("Users",
                new Column("Id", DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity)
            );
        }
    }
}
