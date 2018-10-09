using System.Linq;
using Module01_Introduction_task_01.Entities;

namespace Module01_Introduction_task_01.Context
{
    public interface IRepository<T>
    {
        void Add(T item);
        void Remove(T item);
        IQueryable<T> Query();
    }
}