using System.ComponentModel.DataAnnotations.Schema;

namespace Module01_Introduction_task_01.Entities {
    [Table("Categories")]
    public class Category : BaseEntity {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
    }
}