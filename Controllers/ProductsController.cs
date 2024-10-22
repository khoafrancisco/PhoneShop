

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneShop.Models;

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
        var products = _context.Products.Where(x=>x.Name!.Contains(searchString)).ToList();
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
        var product = _context.Products.Find(ProductID);
        if (product != null)
        {
            var model = new ProductDetailsViewModel();
            model.ProductID = product.ProductID;
            model.Name = product.Name;
            model.Description = product.Description;
            model.Price = product.Price;
            model.Stock = product.Stock;
            model.CategoryID = product.CategoryID;
            model.CreatedDate = product.CreatedDate;
            model.Medias = _context.Medias.Where(m => m.ProductID == ProductID).ToList();

            var relatedProducts = _context.Products.Where(p => p.CategoryID == product.CategoryID && p.ProductID != ProductID).ToList();
            model.RelatedProducts = new List<ProductDetailsViewModel>();
            foreach (var relatedProduct in relatedProducts)
            {
                var relatedProductDetail = new ProductDetailsViewModel();
                relatedProductDetail.ProductID = relatedProduct.ProductID;
                relatedProductDetail.Name = relatedProduct.Name;
                relatedProductDetail.Description = relatedProduct.Description;
                relatedProductDetail.Price = relatedProduct.Price;
                relatedProductDetail.Stock = relatedProduct.Stock;
                relatedProductDetail.CategoryID = relatedProduct.CategoryID;
                relatedProductDetail.CreatedDate = relatedProduct.CreatedDate;
                relatedProductDetail.Medias = _context.Medias.Where(m => m.ProductID == relatedProduct.ProductID).ToList();

                model.RelatedProducts.Add(relatedProductDetail);
            }
            return View(model);
        }
        return NotFound();
    }
}
