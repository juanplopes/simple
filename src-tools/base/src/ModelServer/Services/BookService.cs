using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Entities;
using Example.Project.Domain;

namespace Example.Project.Services
{
    public partial class BookService : EntityService<Book>, IBookService
    {
    }
}