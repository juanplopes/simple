using Simple.Entities;
using Sample.Project.Domain;
using Simple.Services;
using Sample.Project.Services;

namespace Sample.Project.Services
{
    public partial interface IBookService : IEntityService<Book>, IService
    {
    }
}