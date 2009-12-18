using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schema.Metadata.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var schema = new DbSchema("data source=agdocdb; user id=agdoc; password=agdoc",
                "System.Data.OracleClient");

            foreach (var table in schema.GetTables().Where(x => x.TableName.StartsWith("TAGR")))
            {
                //var relations = schema.GetTableRelationsByForeignKey(table.TableSchema, table.TableName);
                //Console.WriteLine(table.TableName);

                foreach (var column in table.PrimaryKeyColumns)
                {
                    Console.WriteLine("\t>" + column.ColumnName + " ( " + column.DisplayTypeName + " ) ");
                }

                foreach (var column in table.GetFields())
                {
                    Console.WriteLine("\t" + column.ColumnName + " ( " + column.DisplayTypeName + " ) ");
                }
                foreach (var relation in table.ForeignKeyColumns)
                {
                    Console.WriteLine("\t ** " + relation.FkColumnName + " ( " + relation.PkTableName + "." + relation.PkColumnName + " ) ");
                }

                foreach (var relation in table.GetForeignKeys())
                {
                    Console.WriteLine("\t ** " + relation.FkName + " ( " + string.Join(", ", relation.Columns.Select(x => x.FkColumnName).ToArray()) + " ) ");
                }

            }


        }
    }
}
