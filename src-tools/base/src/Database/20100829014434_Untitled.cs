using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Migrator.Framework;
using Simple.Migrator.Fluent;
using System.Data;

namespace Example.Project.Database
{
    [Migration(20100829014434)]
    public class Migration20100829014434 : FluentMigration
    {
        public override void Up(SchemaAction schema)
        {
            schema.AddTable("books", t =>
            {
                t.AddString("name");
            });
           
        }

        public override void Down(SchemaAction schema)
        {
            schema.RemoveTable("books");
        }
    }

}