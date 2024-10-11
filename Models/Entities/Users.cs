using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneShop.Models.Entities;

[Table("Users")]
public class Users
{
    [Key]
    public int UserID { get; set; }
    public int UserName { get; set; }
    public int PasswordHash { get; set; }
    public int Role { get; set; }
    public int CreatedDate { get; set; }
}
