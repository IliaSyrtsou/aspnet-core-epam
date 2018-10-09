using System.Linq;
using Northwind.Entities;

namespace Northwind.Context
{
    public interface IRepository<T>
    {
        void Add(T item);
        void Remove(T item);
        IQueryable<T> Query();
    }
}