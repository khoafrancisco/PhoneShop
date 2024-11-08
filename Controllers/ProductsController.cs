

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneShop.Models;
using PhoneShop.Models.Entities;

public class ProductsController : Controller
{
    private readonly AppDbContext _context;
    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(string searchString, int pageNumber = 1, int pageSize = 4)
    {
        if (string.IsNullOrEmpty(searchString))
        {
            searchString = "";
        }
        var products = _context.Products.Where(x => x.Name!.Contains(searchString)).ToList();
        int totalProducts = products.Count();
        var productsOnPage = products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();


        var model = new List<ProductDetailsViewModel>();
        foreach (var product in productsOnPage)
        {
            var productDetail = new ProductDetailsViewModel();
            productDetail.ProductID = product.ProductID;
            productDetail.Name = product.Name;
            productDetail.Description = product.Description;
            productDetail.Price = product.Price;
            productDetail.Stock = product.Stock;
            productDetail.CategoryID = product.CategoryID;
            productDetail.CreatedDate = product.CreatedDate;
            productDetail.MainImage = product.Image;
            productDetail.Medias = _context.Medias.Where(m => m.ProductID == product.ProductID).ToList();

            model.Add(productDetail);
        }

        var viewModel = new ProductListViewModel
        {
            Products = model,
            CurrentPage = pageNumber,
            TotalPages = (int)Math.Ceiling(totalProducts / (double)pageSize),
            SearchString = searchString
        };
        return View(viewModel);
    }
    public IActionResult ProductsDetails(int ProductID)
    {
        var product = _context.Products
            .FirstOrDefault(p => p.ProductID == ProductID);

        if (product == null)
        {
            return NotFound();
        }

        var model = new ProductDetailsViewModel
        {
            ProductID = product.ProductID,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            CategoryID = product.CategoryID,
            CreatedDate = product.CreatedDate,
            MainImage = product.Image,
            Medias = _context.Medias.Where(m => m.ProductID == product.ProductID).ToList(),
            RelatedProducts = new List<ProductDetailsViewModel>() // Khởi tạo danh sách trống
        };

        // Lấy sản phẩm liên quan
        var relatedProducts = _context.Products
            .Where(p => p.CategoryID == product.CategoryID && p.ProductID != ProductID)
            .ToList();

        foreach (var relatedProduct in relatedProducts)
        {
            model.RelatedProducts.Add(new ProductDetailsViewModel
            {
                ProductID = relatedProduct.ProductID,
                Name = relatedProduct.Name,
                Description = relatedProduct.Description,
                Price = relatedProduct.Price,
                Stock = relatedProduct.Stock,
                CategoryID = relatedProduct.CategoryID,
                CreatedDate = relatedProduct.CreatedDate,
                MainImage = relatedProduct.Image,
                Medias = _context.Medias.Where(m => m.ProductID == relatedProduct.ProductID).ToList(),
            });
        }

        return View(model);
    }


}
