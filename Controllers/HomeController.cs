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
            var category = context.Categories.ToList();
            var products = context.Products.Take(8).ToList();
            ViewBag.Categories = category;
            return View(products);
        }

        
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Warranty() {
            return View();
        }
        

        public IActionResult Contact()
        {
            return View();  
        }
        
        public IActionResult About()
        {
            return View();
        }
    }
}
