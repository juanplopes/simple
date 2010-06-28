using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Reflection;
using Simple.Entities;
using Sample.Project.Services;

namespace Sample.Project.Domain
{
    [Serializable]
    public partial class SchemaInfo : Entity<SchemaInfo, ISchemaInfoService>
    {
        public virtual Int64? Version { get; set; } 




        #region ' Generated Helpers '
        static SchemaInfo()
        {
            Identifiers
                .Add(x => x.Version)
;
        }
        
        partial void Initialize();
        
        public static bool operator ==(SchemaInfo obj1, SchemaInfo obj2)
        {
            return object.Equals(obj1, obj2);
        }

        public static bool operator !=(SchemaInfo obj1, SchemaInfo obj2)
        {
            return !(obj1 == obj2);
        }
        
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        
        public SchemaInfo() 
        {  
            Initialize();
        }
        
        public SchemaInfo(Int64? Version) : this()
        {  
            this.Version = Version;
        }
     
        #endregion

    }
}