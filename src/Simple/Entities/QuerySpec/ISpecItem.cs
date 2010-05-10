using System.Linq;

namespace Simple.Entities.QuerySpec
{

    public interface ISpecItem<T> { }

    public interface ISpecItem<T, R> : ISpecItem<T>
    {
        IQueryable<T> Execute(IQueryable<T> query, R resolver);
    }

}
