using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PhoneShop.Models;
using PhoneShop.Models.Entities;
using System.Diagnostics;

namespace PhoneShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index()
        {
            // Fetch the top 5 products
            List<Products> sanpham = context.Products.Take(5).ToList();
            
            // Select each product along with its main image
            var productMedia = sanpham.Select(product => new 
            {
                Product = product,
                Media = context.Medias.FirstOrDefault(media => media.ProductID == product.ProductID && media.IsMain == true)
            }).ToList();

            // Pass the combined product-media data to the view
            ViewData["productMedia"] = productMedia;
            return View();
        }

        
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Warranty() {
            return View();
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Customers(){
            Console.WriteLine("====Hello");
            var customers = context.Customers.ToList();
            return View(customers); 
        }
        
    }
}
