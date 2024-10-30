using System.ComponentModel.DataAnnotations;
namespace PhoneShop.Models;

public class UserLoginModel
{
    [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
    [Display(Name = "Tên đăng nhập")]
    public string UserName { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
    [Display(Name = "Mật khẩu")]
    public string Password { get; set; } = string.Empty;
    [Display(Name = "Ghi nhớ")]
    public bool RememberMe { get; set; }
}