using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Module01_Introduction_task_01.Entities;

namespace Module01_Introduction_task_01.Models {
    public class ProductsModel: PageModel {
        public ICollection<Product> Products { get; set; }
    }
}