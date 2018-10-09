using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Northwind.Entities {
    [Table ("Products")]
    public class Product : BaseEntity {
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        public Decimal UnitPrice { get; set; }
        public Int16 UnitsInStock { get; set; }
        public Int16 UnitsOnOrder { get; set; }
        public Int16 ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        [ForeignKey ("SupplierId")]
        public virtual Supplier Supplier { get; set; }

        [ForeignKey ("CategoryId")]
        public virtual Category Category { get; set; }

    }
}