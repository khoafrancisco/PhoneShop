// Path: PhoneShop/Models/ViewModels/ProductDetailViewModel.cs

using PhoneShop.Models.Entities;

namespace PhoneShop.Models.ViewModels
{
    public class ProductDetailViewModel
    {
        public required Products Product { get; set; }
        public List<Media>? Medias { get; set; }
    }
}
