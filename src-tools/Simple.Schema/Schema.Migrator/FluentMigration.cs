using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Migrator.Framework;

namespace Schema.Migrator
{
    public abstract class FluentMigration : Migration
    {
        private IConvention _convention = null;
        public virtual IConvention Convention
        {
            get
            {
                if (_convention == null)
                    _convention = new DefaultConvention();

                return _convention;
            }
            set { _convention = value; }
        }

        private SchemaBuilder _schemaBuilder;
        public virtual SchemaBuilder Schema
        {
            get
            {
                if (_schemaBuilder == null)
                    _schemaBuilder = new SchemaBuilder(Database, Convention);

                return _schemaBuilder;
            }
            set
            {
                _schemaBuilder = value;
            }
        }
    }
}
