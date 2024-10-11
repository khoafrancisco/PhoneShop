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
    public int OrderDate { get; set; }
    public int TotalAmount { get; set; }
    public int PaymentStatus { get; set; }
    public int ShippingAddress { get; set; }
    public int Discount { get; set; }
}
