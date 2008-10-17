using System;
using System.Collections.Generic;
using System.Text;
using BasicLibrary.Configuration;

namespace BasicLibrary.LibraryConfig
{
    public class SqlLockingElement : ConfigElement
    {
        [ConfigElement("connectionString", Required = true)]
        public string ConnectionString { get; set; }
        [ConfigElement("tableName", Required = true)]
        public string TableName { get; set; }
        [ConfigElement("typeColumn", Required = true)]
        public string TypeColumn { get; set; }
        [ConfigElement("idColumn", Required = true)]
        public string IdColumn { get; set; }
        [ConfigElement("semaphoreColumn", Required = true)]
        public string SemaphoreColumn { get; set; }
        [ConfigElement("data", Required = true)]
        public string DataColumn { get; set; }
        [ConfigElement("secondsToWait", Default = 10)]
        public int SecondsToWait { get; set; }
    }
}
