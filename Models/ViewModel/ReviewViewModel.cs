using System.ComponentModel.DataAnnotations;

namespace PhoneShop.Models.ViewModels
{
    public class ReviewViewModel
    {
        public string? CustomerName { get; set; } // Tên khách hàng
        public int Rating { get; set; } // Điểm đánh giá (1-5 sao)
        public string? Comment { get; set; } // Nội dung nhận xét
        public DateTime CreatedDate { get; set; } // Ngày đánh giá
    }
}
