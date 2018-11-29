using System;
using System.ComponentModel.DataAnnotations;

namespace Northwind.Web.Models.Api
{
    public class CreateProductModel
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public int? SupplierId { get; set; }
        public string SupplierName {get;set;}
        [Required]
        public int? CategoryId { get; set; }
        public string CategoryName {get;set;}
        [Required]
        public string QuantityPerUnit { get; set; }
        [Required]
        public Decimal? UnitPrice { get; set; }
        [Required]
        public Int16? UnitsInStock { get; set; }
        public Int16? UnitsOnOrder { get; set; }
        public Int16? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
    }
}