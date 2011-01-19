using Simple.Entities;
using Example.Project.Domain;
using Simple.Services;
using Example.Project.Services;

namespace Example.Project.Services
{
    public partial interface IBookService : IEntityService<Book>, IService
    {
    }
}