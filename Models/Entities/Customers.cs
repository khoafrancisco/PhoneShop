using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneShop.Models.Entities;

[Table("Customers")]
public class Customers
{
    [Key]
    public int CustomerID { get; set; }
    public int FullName { get; set; }
    public int Email { get; set; }
    public int Phone { get; set; }
    public int Address { get; set; }
    public int CreatedDate { get; set; }
}
