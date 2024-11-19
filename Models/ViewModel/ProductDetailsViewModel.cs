using PhoneShop.Models.Entities;
using PhoneShop.Models.ViewModels;

public class ProductDetailsViewModel
{
    public int ProductID { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int CategoryID { get; set; }
    public DateTime CreatedDate { get; set; }
    public string? MainImage { get; set; }
    public List<Media>? Medias { get; set; } = new List<Media>();
    public List<ProductDetailsViewModel>? RelatedProducts { get; set; } = new List<ProductDetailsViewModel>();

    // Thuộc tính liên quan đến đánh giá
    public double AverageRating { get; set; } // Điểm trung bình
    public List<ReviewViewModel>? Reviews { get; set; } = new List<ReviewViewModel>(); // Danh sách đánh giá
    public Dictionary<int, int>? RatingBreakdown { get; set; } = new Dictionary<int, int>(); // Phân tích đánh giá (1-5 sao)
}

public class ReviewViewModel
{
    public string ? CustomerName { get; set; }
    public string ? CustomerEmail { get; set; }
    public int Rating { get; set; }
    public string ? Comment { get; set; }
    public DateTime CreatedDate { get; set; }
}