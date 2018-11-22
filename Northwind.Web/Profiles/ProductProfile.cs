using AutoMapper;
using Northwind.Entities;
using Northwind.Web.Models;
using Northwind.Web.Models.Api;

namespace Northwind.Web.Profiles
{
    public class ProductProfile: Profile
    {
        public ProductProfile() {
            CreateMap<Product, CreateProductViewModel>()
                .ForMember(x => x.CategoryName, m => m.MapFrom(u => u.Category.CategoryName))
                .ForMember(x => x.CategoryId, m => m.MapFrom(u => u.Category.CategoryId))
                .ForMember(x => x.CompanyName, m => m.MapFrom(u => u.Supplier.CompanyName))
                .ForMember(x => x.SupplierId, m => m.MapFrom(u => u.Supplier.SupplierId));
            CreateMap<CreateProductViewModel, Product>();
            CreateMap<Product, EditProductViewModel>()
                .ForMember(x => x.CategoryName, m => m.MapFrom(u => u.Category.CategoryName))
                .ForMember(x => x.CategoryId, m => m.MapFrom(u => u.Category.CategoryId))
                .ForMember(x => x.CompanyName, m => m.MapFrom(u => u.Supplier.CompanyName))
                .ForMember(x => x.SupplierId, m => m.MapFrom(u => u.Supplier.SupplierId));
            CreateMap<EditProductViewModel, Product>();
            CreateMap<Product, ProductModel>()
                .ForMember(x => x.SupplierName, m => m.MapFrom(u => u.Supplier.CompanyName))
                .ForMember(x => x.CategoryName, m => m.MapFrom(u => u.Category.CategoryName));
        }
    }
}