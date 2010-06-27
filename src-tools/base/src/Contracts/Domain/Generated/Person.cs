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
    public partial class Person : Entity<Person, IPersonService>
    {
        public virtual Int32? Id { get; set; } 




        #region ' Generated Helpers '
        static Person()
        {
            Identifiers
                .Add(x => x.Id)
;
        }
        
        partial void Initialize();
        
        public static bool operator ==(Person obj1, Person obj2)
        {
            return object.Equals(obj1, obj2);
        }

        public static bool operator !=(Person obj1, Person obj2)
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
        
        public Person() 
        {  
            Initialize();
        }
        
        public Person(Int32? Id) : this()
        {  
            this.Id = Id;
        }
     
        #endregion

    }
}