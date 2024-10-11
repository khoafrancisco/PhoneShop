using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneShop.Models.Entities;

[Table("Shipping")]
public class Shipping
{
    [Key]
    public int ShippingID { get; set; }
    public int OrderID { get; set; }
    [ForeignKey("OrderID")]
    public int ShippingMethod { get; set; }
    public int ShippingStatus { get; set; }
    public int ShippingDate { get; set; }
    public int ExpectedDeliveryDate { get; set; }
}
