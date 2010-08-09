using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Migrator.Framework;

namespace Simple.Migrator.Fluent
{
    public abstract class FluentMigration : Migration
    {
        public virtual SchemaAction BuildSchema()
        {
            return new SchemaAction(new DefaultConvention());
        }

        public virtual void ExecuteSchema(SchemaAction action)
        {
            action.Execute(Database);
        }

        public abstract void Up(SchemaAction schema);
        public abstract void Down(SchemaAction schema);

        public override void Up()
        {
            var schema = BuildSchema();
            Up(schema);
            ExecuteSchema(schema);
        }

        public override void Down()
        {
            var schema = BuildSchema();
            Down(schema);
            ExecuteSchema(schema);
        }
    }
}
