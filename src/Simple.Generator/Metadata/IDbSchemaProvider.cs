using System.Data;
using System;
using System.Collections.Generic;

namespace Simple.Metadata
{
    public interface IDbSchemaProvider : IDisposable
    {
        MetaContext Context { get; }

        IEnumerable<DbTable> GetTables(IList<string> includedTables, IList<string> excludedTables);
        IEnumerable<DbRelation> GetConstraints(IList<string> includedTables, IList<string> excludedTables);
        IEnumerable<DbColumn> GetColumns(DbTableName table);
        string QualifiedTableName(DbTableName table);
        
        DbType GetDbColumnType(string providerDbType);
    }
}
