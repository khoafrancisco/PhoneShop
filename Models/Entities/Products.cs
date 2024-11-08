using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneShop.Models.Entities
{
    [Table("Products")]
    public class Products
    {
        [Key]
        public int ProductID { get; set; }
        
        public string? Name { get; set; }
        
        public string? Description { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        
        public string? Image { get; set; }  // Thêm trường Image
        
        public int Stock { get; set; }
        
        public int CategoryID { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        // Navigation property for Category
        public Category? Category { get; set; }
    }
}
