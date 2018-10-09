using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Northwind.Entities;
using Northwind.Models;

namespace Module01_Introduction_task_01.Profiles
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