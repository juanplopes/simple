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
    public partial class Book : Entity<Book, IBookService>
    {
        public virtual Int32? Id { get; set; } 

        public virtual String Title { get; set; } 
        public virtual String Author { get; set; } 



        #region ' Generated Helpers '
        static Book()
        {
            Identifiers
                .Add(x => x.Id)
;
        }
        
        partial void Initialize();
        
        public static bool operator ==(Book obj1, Book obj2)
        {
            return object.Equals(obj1, obj2);
        }

        public static bool operator !=(Book obj1, Book obj2)
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
        
        public Book() 
        {  
            Initialize();
        }
        
        public Book(Int32? Id) : this()
        {  
            this.Id = Id;
        }
     
        #endregion

    }
}