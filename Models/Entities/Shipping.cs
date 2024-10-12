using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace PhoneShop.Models.Entities;

[Table("Shipping")]
public class Shipping
{
    [Key]
    public int ShippingID { get; set; }
    public int OrderID { get; set; }
    [ForeignKey("OrderID")]
    public string? ShippingMethod { get; set; }
    public String? ShippingStatus { get; set; }
    public DateTime ShippingDate { get; set; }
    public DateTime ExpectedDeliveryDate { get; set; }
}
