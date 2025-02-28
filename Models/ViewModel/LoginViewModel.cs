using System.ComponentModel.DataAnnotations;

namespace PhoneShop.Models.ViewModels
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [Phone(ErrorMessage = "Số điện thoại không đúng định dạng")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string Password { get; set; } = string.Empty;
    }
}
