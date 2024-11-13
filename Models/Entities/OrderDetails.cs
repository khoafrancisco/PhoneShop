using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneShop.Models.Entities;

[Table("OrderDetails")]
public class OrderDetails
{
    [Key]
    public int OrderDetailID { get; set; }

    public int OrderID { get; set; }
    [ForeignKey("OrderID")]
    public virtual Orders ? Order { get; set; }  // Navigation property to Orders

    public int ProductID { get; set; }
    [ForeignKey("ProductID")]
    public virtual Products ? Product { get; set; }  // Navigation property to Product

    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
