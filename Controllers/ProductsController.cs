

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

    public IActionResult Index()
    {
        var products = _context.Products.ToList();
        var model = new List<ProductDetailsViewModel>();
        foreach (var product in products)
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
        return View(model);
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
            return View(model);
        }
        return NotFound();
    }
}
