using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using Simple.Metadata;
using Sample.Project.Config;
using Simple;
using Simple.NVelocity;

namespace Sample.Project.Generator.Runners
{
    public abstract class BaseTableGenerator : IGenerator
    {
        public IList<string> TableNames { get; set; }
        private IList<DbTable> _tables = null;
        public IList<DbTable> Tables
        {
            get
            {
                if (_tables == null)
                {
                    _tables = GetTables();
                }

                return _tables;
            }
            set { _tables = value; }
        }

        private IList<DbTable> GetTables()
        {
            var names = GetTransformedTableNames();

            var excluded = names.Where(x => x.StartsWith("-")).ToList();
            var included = names.Except(excluded).ToList();

            var db = new DbSchema(
                Simply.Do.GetConfig<ApplicationConfig>().ADOProvider,
                Simply.Do.GetConnectionString());

            return db.GetTables(included, excluded.Select(x => x.Substring(1)).ToList()).ToList();
        }

        private IList<string> GetTransformedTableNames()
        {
            var names = TableNames ?? Default.TableNames.ToList();
            if (names.Count == 1 && names[0] == "$")
                names = Default.TableNames.ToList();
            return names.Select(x => x.Replace("*", "%")).ToList();
        }

        public virtual void Execute()
        {
            foreach (var table in Tables)
                ExecuteSingle(table);
        }


        public abstract void ExecuteSingle(DbTable table);
        public abstract string FilePath(string className);
        public abstract void Delete(string className);
    }
}
