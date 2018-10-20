using AutoMapper;
using Northwind.Entities;
using Northwind.Models;

namespace Module01_Introduction_task_01.Profiles
{
    public class ProductProfile: Profile
    {
        public ProductProfile() {
            CreateMap<Product, ProductModel>()
                .ForMember(x => x.CategoryName, m => m.MapFrom(u => u.Category.CategoryName))
                .ForMember(x => x.CompanyName, m => m.MapFrom(u => u.Supplier.CompanyName));
        }
    }
}