//
//  Data.Common.DbSchema - http://dbschema.codeplex.com
//
//  The contents of this file are subject to the New BSD
//  License (the "License"); you may not use this file
//  except in compliance with the License. You may obtain a copy of
//  the License at http://www.opensource.org/licenses/bsd-license.php
//
//  Software distributed under the License is distributed on an 
//  "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either express or
//  implied. See the License for the specific language governing
//  rights and limitations under the License.
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace Simple.Metadata
{
    public class DbSchema : DbObject
    {
        private DataTable _tableCache = null;
        public DbSchema(string provider, string connectionString)
            : base(provider, connectionString)
        {
        }

        public DbSchema(IDbSchemaProvider provider)
            : base(provider)
        {
        }


        private DataTable GetSchemaTables()
        {
            if (_tableCache != null) return _tableCache;
            return _tableCache = Provider.GetSchemaTables();
        }

        public IEnumerable<DbTable> GetTables()
        {
            return GetTables("%");
        }

        public IEnumerable<DbTable> GetTables(params string[] included)
        {
            return GetTables(included, new string[] { });
        }

        public IEnumerable<DbTable> GetTables(IList<string> included, IList<string> excluded)
        {
            return GetGeneric(included, excluded, "TABLE", "BASE TABLE");
        }

        public IEnumerable<DbTable> GetViews()
        {
            return GetViews("%");
        }

        public IEnumerable<DbTable> GetViews(params string[] included)
        {
            return GetViews(included, new string[] { });
        }

        public IEnumerable<DbTable> GetViews(IList<string> included, IList<string> excluded)
        {
            return GetGeneric(included, excluded, "VIEW");
        }

        public IEnumerable<DbTable> GetGeneric(IList<string> included, IList<string> excluded, params string[] types)
        {
            var incString = included.Count > 0 ?
                string.Join(" OR ", included.Select(x => GetTableWhereClause("LIKE", x)).ToArray()) :
                "1=1";

            var excString = excluded.Count > 0 ?
                string.Join(" AND ", excluded.Select(x => GetTableWhereClause("NOT LIKE", x)).ToArray()) :
                "1=1";

            var typesString = types.Length > 0 ?
                string.Join(" OR ", types.Select(x => "TABLE_TYPE = '" + x + "'").ToArray()) :
                "1=1";

            foreach (var row in GetSchemaTables().Select(string.Format("({0}) AND ({1}) AND ({2})", incString, excString, typesString)))
                yield return new DbTable(Provider, row);
        }

        protected string GetTableWhereClause(string op, string tableName)
        {
            var columns = new[] { "TABLE_NAME", "TABLE_SCHEMA", "TABLE_CATALOG" };
            var names = tableName.Split('.').Reverse().ToList();

            var clauses = new List<string>();
            for (int i = 0; i < columns.Length && i < names.Count; i++)
                clauses.Add(string.Format("{0} {1} '{2}'", columns[i], op, names[i]));

            return "(" + string.Join(" AND ", clauses.ToArray()) + ")";
        }

        #region ' Helpers '

        public string GetDatabaseName()
        {
            return Provider.GetDatabaseName();
        }

        #endregion

    }
}
