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

    public IActionResult Index(int category, string searchString, int pageNumber = 1, int pageSize = 8)
    {
        Console.WriteLine("Category: " + category);

        if (string.IsNullOrEmpty(searchString))
        {
            searchString = "";
        }

        // Lọc sản phẩm theo searchString và category
        var filteredProducts = _context.Products
            .Where(x => x.Name!.Contains(searchString) && (x.CategoryID == category || category == 0));

        // Tính tổng số sản phẩm sau khi lọc
        int totalProducts = filteredProducts.Count();

        // Thực hiện phân trang
        var productsOnPage = filteredProducts
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        // Chuyển đổi dữ liệu sang ProductDetailsViewModel
        var model = new List<ProductDetailsViewModel>();
        foreach (var product in productsOnPage)
        {
            var productDetail = new ProductDetailsViewModel
            {
                ProductID = product.ProductID,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                CategoryID = product.CategoryID,
                CreatedDate = product.CreatedDate,
                MainImage = product.Image,
                Medias = _context.Medias.Where(m => m.ProductID == product.ProductID).ToList()
            };

            model.Add(productDetail);
        }

        // Tạo ProductListViewModel với dữ liệu phân trang
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
        // Lấy thông tin sản phẩm
        var product = _context.Products
            .FirstOrDefault(p => p.ProductID == ProductID);

        if (product == null)
        {
            return NotFound();
        }

        // Lấy danh sách review liên quan đến sản phẩm
        var reviews = _context.Reviews
            .Where(r => r.ProductID == ProductID)
            .Include(r => r.Customer) // Bao gồm thông tin khách hàng
            .ToList();

        // Tạo ProductDetailsViewModel
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
            RelatedProducts = new List<ProductDetailsViewModel>(), // Khởi tạo danh sách trống
            Reviews = reviews.Select(r => new ReviewViewModel
            {
                // CustomerName = r.Customer.FullName,
                // CustomerEmail = r.Customer.Email,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedDate = r.CreatedDate
            }).ToList()
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

    // Thêm review
    // [HttpPost]
    // public IActionResult AddReview(ProductDetailsViewModel input)
    // {
    //     if (ModelState.IsValid)
    //     {
    //         var review = new Reviews
    //         {
    //             ProductID = input.ProductID,
    //             CustomerID = input.CustomerID, // Giả sử đã có ID khách hàng đăng nhập
    //             Rating = input.Rating,
    //             Comment = input.Comment,
    //             CreatedDate = DateTime.Now
    //         };

    //         _context.Reviews.Add(review);
    //         _context.SaveChanges();

    //         return RedirectToAction("ProductsDetails", new { ProductID = input.ProductID });
    //     }

    //     return BadRequest("Dữ liệu không hợp lệ");
    // }
}
