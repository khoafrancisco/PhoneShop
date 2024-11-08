using System.ComponentModel.DataAnnotations;

namespace PhoneShop.Models.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Email không được để trống")]
    [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Mật khẩu không được để trống")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu")]
    [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không khớp")]
    public string ConfirmPassword { get; set; } = string.Empty;

    public string? Phone { get; set; }  // Optional fields with default null values
    public string? Address { get; set; } // Optional fields with default null values
}
