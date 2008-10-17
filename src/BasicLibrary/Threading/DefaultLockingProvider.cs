using System;
using System.Collections.Generic;
using System.Text;
using BasicLibrary.LibraryConfig;

namespace BasicLibrary.Threading
{
    public class DefaultLockingProvider : SqlLockingProvider
    {
        BasicLibraryConfig config = BasicLibraryConfig.Get();
        
        protected override string ConnectionString
        {
            get { return config.Threading.DefaultLockingProvider.ConnectionString; }
        }

        protected override string TableName
        {
            get { return config.Threading.DefaultLockingProvider.TableName; }
        }

        protected override string TypeColumn
        {
            get { return config.Threading.DefaultLockingProvider.TypeColumn; }
        }

        protected override string IdColumn
        {
            get { return config.Threading.DefaultLockingProvider.IdColumn; }
        }

        protected override string SemaphoreColumn
        {
            get { return config.Threading.DefaultLockingProvider.SemaphoreColumn; }
        }

        protected override string DataColumn
        {
            get { return config.Threading.DefaultLockingProvider.DataColumn; }
        }

        protected override int SecondsToWait
        {
            get { return config.Threading.DefaultLockingProvider.SecondsToWait; }
        }
    }
}
