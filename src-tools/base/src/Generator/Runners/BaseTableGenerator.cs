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
        public bool DeleteFlag { get; set; }
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

            var ret = db.GetTables(included, excluded.Select(x => x.Substring(1)).ToList()).ToList();

            Simply.Do.Log(this).DebugFormat("Found tables: {0}", string.Join(", ", ret.Select(x => x.Name).ToArray()));

            return ret;
        }

        private IList<string> GetTransformedTableNames()
        {
            var names = TableNames ?? Default.TableNames.ToList();
            
            if (names.Remove("$")) 
                foreach (var name in Default.TableNames) 
                    names.Add(name);

            var ret = names.Select(x => x.Replace("*", "%")).ToArray();
            return ret;
        }

        public virtual void Execute()
        {
            foreach (var table in Tables)
            {
                if (!DeleteFlag)
                    Create(table);
                else
                    Delete(table);
            }
        }


        public abstract void Create(DbTable table);
        public abstract void Delete(DbTable table);
    }
}
