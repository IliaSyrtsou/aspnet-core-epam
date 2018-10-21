using System;
using System.ComponentModel.DataAnnotations;

namespace Northwind.Models {
    public class EditProductViewModel {
        [Required]
        public int? ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        public string CompanyName { get; set; }
        public string CategoryName { get; set; }
        [Required]
        public int? SupplierId { get; set; }
        [Required]
        public int? CategoryId { get; set; }
        [Required]
        public string QuantityPerUnit { get; set; }
        [Required]
        public Decimal? UnitPrice { get; set; }
        [Required]
        public Int16? UnitsInStock { get; set; }
        public Int16 UnitsOnOrder { get; set; }
        public Int16 ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
    }
}