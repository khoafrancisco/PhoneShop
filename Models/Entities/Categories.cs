using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneShop.Models.Entities;

[Table("Categories")]
public class Category
{
    [Key]
    public int CategoryID { get; set; }
    public required string CategoryName { get; set; }
    
}
