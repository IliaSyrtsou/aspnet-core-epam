using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Module01_Introduction_task_01.Entities;

namespace Module01_Introduction_task_01.Models {
    public class CategoriesModel : PageModel {
        public ICollection<Category> Categories { get; set; }
    }
}