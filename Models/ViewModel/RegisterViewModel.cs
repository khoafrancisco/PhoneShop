using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PhoneShop.Models.ViewModels
{
    public class RegisterViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "Vui lòng nhập email hoặc số điện thoại.")]
        [Display(Name = "Email hoặc Số điện thoại")]
        public string EmailOrPhone { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu.")]
        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu và xác nhận mật khẩu không khớp.")]
        public string ConfirmPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            // Define email and phone patterns
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            var phonePattern = @"^[0-9]{10,15}$";

            if (!Regex.IsMatch(EmailOrPhone ?? string.Empty, emailPattern) &&
                !Regex.IsMatch(EmailOrPhone ?? string.Empty, phonePattern))
            {
                results.Add(new ValidationResult("Vui lòng nhập đúng định dạng email hoặc số điện thoại.", new[] { "EmailOrPhone" }));
            }

            return results;
        }
    }
}
