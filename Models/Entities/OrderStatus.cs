using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneShop.Models.Entities;

[Table("OrderStatus")]
public class OrderStatus
{
    [Key]
    public int StatusID { get; set; }
    public int OrderID { get; set; }
    [ForeignKey("OrderID")]
    public string? Status { get; set; }
    public DateTime UpdatedDate { get; set; }
}
