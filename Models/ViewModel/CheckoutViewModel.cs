using System;
using System.ComponentModel.DataAnnotations;

namespace PhoneShop.Models
{
    public class CheckoutViewModel
    {
        [Key]
        public int CustomerID { get; set; }

        // Receiver Information
        [Required(ErrorMessage = "Họ và tên là bắt buộc.")]
        [Display(Name = "Họ và tên")]
        public string ? FullName { get; set; }

        [Required(ErrorMessage = "Số điện thoại là bắt buộc.")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        [Display(Name = "Số điện thoại")]
        public string ? Phone { get; set; }

        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        [Display(Name = "Email")]
        public string ? Email { get; set; }

        [Display(Name = "Địa chỉ")]
        public string ? Address { get; set; }

        // Additional Fields for Checkout
        [Display(Name = "Lời nhắn")]
        public string ? Note { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn hình thức nhận hàng.")]
        [Display(Name = "Hình thức nhận hàng")]
        public string ? DeliveryMethod { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn tỉnh / thành phố.")]
        [Display(Name = "Tỉnh / Thành phố")]
        public string ? Province { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn quận / huyện.")]
        [Display(Name = "Quận / Huyện")]
        public string ? District { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn phường / xã.")]
        [Display(Name = "Phường / Xã")]
        public string ? Ward { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn phương thức thanh toán.")]
        [Display(Name = "Phương thức thanh toán")]
        public string ? PaymentMethod { get; set; }

        [Display(Name = "Tổng tiền")]
        public decimal TotalAmount { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
