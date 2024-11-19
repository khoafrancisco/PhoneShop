using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneShop.Models.Entities;

[Table("Reviews")]
public class Reviews
{
    [Key]
    public int ReviewID { get; set; }
    
    public int ProductID { get; set; }
    [ForeignKey("ProductID")]
    public Products ? Product { get; set; } // Navigation Property to Product
    
    public int CustomerID { get; set; }
    [ForeignKey("CustomerID")]
    public Customers ? Customer { get; set; } // Navigation Property to Customer
    
    [Range(1, 5)]
    public int Rating { get; set; }
    
    public string? Comment { get; set; }
    
    public DateTime CreatedDate { get; set; }
}
