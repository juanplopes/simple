using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Reflection;
using Simple.Entities;
using Example.Project.Services;

namespace Example.Project.Domain
{
    [Serializable]
    public partial class Author : Entity<Author, IAuthorService>
    {
        public virtual Int32 Id { get; set; } 

        public virtual String Name { get; set; } 


        public virtual ICollection<Book> Books { get; set; } 

        #region ' Generated Helpers '
        static Author()
        {
            Identifiers
                .Add(x => x.Id)
;
        }
        
        partial void Initialize();
        
        public static bool operator ==(Author obj1, Author obj2)
        {
            return object.Equals(obj1, obj2);
        }

        public static bool operator !=(Author obj1, Author obj2)
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
        
        public Author() 
        {
            this.Books = new HashSet<Book>();
            Initialize();
        }
        
        public override Author Clone()
        {
            var cloned = base.Clone();
            cloned.Books = null;
            return cloned;
        }

        public Author(Int32 Id) : this()
        {  
            this.Id = Id;
        }
     
        #endregion

    }
}