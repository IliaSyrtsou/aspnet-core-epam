using System;
using System.Linq;
using Northwind.Entities;
namespace Northwind.Services.Interfaces
{
    public interface IBaseService<T> where T: BaseEntity
    {
        IQueryable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        void SaveChanges();
    }
}