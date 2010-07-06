using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Migrator.Framework;
using Simple.Migrator.Fluent;
using System.Data;

namespace Sample.Project.Tools.Migrations
{
    [Migration(20100706134722)]
    public class Migration20100706134722 : FluentMigration
    {
        public override void Up()
        {
            Schema.AddTable("books", t =>
            {
                t.AddString("title");
                t.AddString("author");
            });

        }

        public override void Down()
        {
            Schema.RemoveTable("books");
        }
    }

}