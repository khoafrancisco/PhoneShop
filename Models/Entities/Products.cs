using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneShop.Models.Entities;

[Table("Products")]
public class Products
{
    [Key]
    public int ProductID { get; set; }
    public int Name { get; set; }
    public int Description { get; set; }
    public int Price { get; set; }
    public int Stock { get; set; }
    public int CategoryID { get; set; }
    [ForeignKey("CategoryID")]
    public int CreatedDate { get; set; }

}
