using PhoneShop.Models.Entities;

public class ProductDetailsViewModel
{
    public int ProductID { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int CategoryID { get; set; }
    public DateTime CreatedDate { get; set; }
    public List<Media>? Medias { get; set; }

}
