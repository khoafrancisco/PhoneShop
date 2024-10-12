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
    public int CustomerID { get; set; }
    [ForeignKey("CustomerID")]
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedDate { get; set; }
    
}
