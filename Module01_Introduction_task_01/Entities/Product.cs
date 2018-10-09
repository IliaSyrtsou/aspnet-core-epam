using System.ComponentModel.DataAnnotations.Schema;

namespace Module01_Introduction_task_01.Entities {
    [Table("Products")]
    public class Product : BaseEntity {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
    }
}