using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Northwind.Entities;

namespace Northwind.Models {
    public class ProductsModel: PageModel {
        public ICollection<Product> Products { get; set; }
    }
}