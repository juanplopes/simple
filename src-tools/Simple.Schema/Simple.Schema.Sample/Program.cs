using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Meta.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var schema = new DbSchema("data source=agdocdb; user id=agdoc; password=agdoc",
                "System.Data.OracleClient");

            foreach (var table in schema.GetTables().Where(x => x.TableName.StartsWith("TAGR")))
            {
                var relations = schema.GetTableRelationsByForeignKey(table.TableSchema, table.TableName);
                Console.WriteLine(table.TableName);
                foreach (var column in table.VisibleColumns)
                {
                    Console.WriteLine("\t" + column.ColumnName + " ( " + column.DisplayTypeName + " ) ");
                }
            }


        }
    }
}
