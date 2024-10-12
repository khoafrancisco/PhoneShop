using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneShop.Models.Entities;

[Table("Products")]
public class Products
{
    [Key]
    public int ProductID { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int CategoryID { get; set; }
    [ForeignKey("CategoryID")]
    public DateTime CreatedDate { get; set; }

}
