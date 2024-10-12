using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneShop.Models.Entities;

[Table("Users")]
public class Users
{
    [Key]
    public int UserID { get; set; }
    public string? UserName { get; set; }
    public string? PasswordHash { get; set; }
    public string? Role { get; set; }
    public DateTime CreatedDate { get; set; }
}
