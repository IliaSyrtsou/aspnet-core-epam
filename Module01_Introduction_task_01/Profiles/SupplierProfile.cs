using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Northwind.Entities;

namespace Module01_Introduction_task_01.Profiles
{
    public class SupplierProfile: Profile
    {
        public SupplierProfile() {
            CreateMap<Supplier, SelectListItem>()
                .ForMember(x => x.Text, m => m.MapFrom(u => u.CompanyName))
                .ForMember(x => x.Value, m => m.MapFrom(u => u.SupplierId));
        }
    }
}