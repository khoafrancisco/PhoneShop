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
            List<Products> sanpham = context.Products.Take(5).ToList();
            ViewData["sanpham"] = sanpham;
            
            List<Media> medias = context.Medias.Take(5).ToList();
            ViewData["hinhanh"] = medias;
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
