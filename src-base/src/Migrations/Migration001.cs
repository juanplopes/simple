using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Migrator.Framework;
using System.Data;

namespace Conspirarte.Migrations
{
    [Migration(1)]
    public class Migration001 : Migration
    {
        public override void Up()
        {
            Database.AddTable("Users",
                new Column("Login", DbType.String, 30),
                new Column("Password", DbType.Binary, 64)
            );
        }

        public override void Down()
        {
            Database.RemoveTable("Users");
        }
    }
}
