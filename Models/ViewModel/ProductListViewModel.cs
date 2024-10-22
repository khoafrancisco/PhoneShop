using PhoneShop.Models.Entities;
public class ProductListViewModel
{
    public List<ProductDetailsViewModel>? Products { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public string? SearchString { get; set; } 
}