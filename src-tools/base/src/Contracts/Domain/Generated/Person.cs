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
        public virtual Byte[] Id2 { get; set; } 

        public virtual String Asd { get; set; } 



        #region ' Generated Helpers '
        static Person()
        {
            Identifiers
                .Add(x => x.Id)
                .Add(x => x.Id2)
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
        
        public Person(Int32? Id, Byte[] Id2) : this()
        {  
            this.Id = Id;
            this.Id2 = Id2;
        }
     
        #endregion

    }
}