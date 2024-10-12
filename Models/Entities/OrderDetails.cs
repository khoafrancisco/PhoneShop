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
    public int ProductID { get; set; }
    [ForeignKey("ProductID")]
    public int Quantity { get; set; }
    public decimal Price { get; set; }

}
