using System;
using System.ComponentModel.DataAnnotations;

namespace Northwind.Models {
    public class ProductModel {
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        public Decimal UnitPrice { get; set; }
        public Int16 UnitsInStock { get; set; }
        public Int16 UnitsOnOrder { get; set; }
        public Int16 ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
    }
}