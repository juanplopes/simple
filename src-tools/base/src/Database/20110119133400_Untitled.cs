using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Migrator.Framework;
using Simple.Migrator.Fluent;
using System.Data;

namespace Example.Project.Database
{
    [Migration(20110119133400)]
    public class Migration20110119133400 : FluentMigration
    {
        public override void Up(SchemaAction schema)
        {
            schema.AddTable("authors", t =>
            {
                t.AddString("name");
            });

            schema.ChangeTable("books", t =>
            {
                t.AddInt32("author_id").AutoForeignKey("authors");
            });

        }

        public override void Down(SchemaAction schema)
        {
            schema.ChangeTable("books", t =>
            {
                t.RemoveColumn("author_id");
            });

            schema.RemoveTable("authors");
        }
    }

}