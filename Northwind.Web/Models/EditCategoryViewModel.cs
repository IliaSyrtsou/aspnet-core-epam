using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Northwind.Web.Models {
    public class EditCategoryViewModel {
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
    }
}