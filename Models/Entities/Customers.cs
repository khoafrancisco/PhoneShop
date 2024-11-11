using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneShop.Models.Entities;

[Table("Customers")]
public class Customers
{
    [Key]
    public int CustomerID { get; set; }
    public string? FullName { get; set; } // Dấu hỏi chấm ở đây có ý nghĩa là thuộc tính này có thể có giá trị null
    public string ? Email { get; set; }
    public string ? Password { get; set; }
    public string ? Phone { get; set; }
    public string ? Address { get; set; }
    public DateTime CreatedDate { get; set; }
}
