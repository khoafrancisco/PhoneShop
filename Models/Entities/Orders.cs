using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneShop.Models.Entities;

[Table("Orders")]
public class Orders
{
    [Key]
    public int OrderID { get; set; }
    public int CustomerID { get; set; }
    [ForeignKey("CustomerID")]
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string? PaymentStatus { get; set; }
    public string? ShippingAddress { get; set; }
    public decimal Discount { get; set; }
}
