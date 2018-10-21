using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Northwind.Entities;
using Northwind.Web.Models;

namespace Northwind.Web.Profiles
{
    public class CategoryProfile: Profile
    {
        public CategoryProfile() {
            CreateMap<Category, SelectListItem>()
                .ForMember(x => x.Text, m => m.MapFrom(u => u.CategoryName))
                .ForMember(x => x.Value, m => m.MapFrom(u => u.CategoryId));
        }
    }
}