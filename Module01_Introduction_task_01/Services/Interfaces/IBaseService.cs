using System;
using System.Linq;
using Northwind.Entities;
namespace Northwind.Services.Interfaces
{
    public interface IBaseService<T> where T: BaseEntity
    {
        IQueryable<T> GetAll();
    }
}