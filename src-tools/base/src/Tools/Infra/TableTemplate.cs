using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using Simple.Metadata;
using Sample.Project.Config;
using Simple;
using Simple.NVelocity;

namespace Sample.Project.Tools.Infra
{
    public abstract class TableTemplate : ICommand
    {
        public IList<string> TableNames { get; set; }
        public bool DeleteFlag { get; set; }

        private IList<DbTable> _tables = null;
        public IList<DbTable> Tables
        {
            get
            {
                if (_tables == null) _tables = GetTables();
                return _tables;
            }
            set { _tables = value; }
        }

        private IList<DbTable> GetTables()
        {
            using (Context.Development)
            {

                var config = Simply.Do.GetConfig<ApplicationConfig>();
                var db = new DbSchema(config.ADOProvider, Simply.Do.GetConnectionString());

                var names = new TableNameTransformer(Default.TableNames)
                    .Transform(TableNames);

                var ret = db.GetTables(names.Included, names.Excluded).ToList();

                Simply.Do.Log(this).InfoFormat("Found tables: {0}", string.Join(", ", ret.Select(x => x.Name).ToArray()));

                return ret;
            }
        }
        public virtual void Execute()
        {
            foreach (var table in Tables)
                if (!DeleteFlag)
                    Create(table);
                else
                    Delete(table);
        }


        public abstract void Create(DbTable table);
        public abstract void Delete(DbTable table);
    }
}
