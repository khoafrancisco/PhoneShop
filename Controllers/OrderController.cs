using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PhoneShop.Models;
using PhoneShop.Models.Entities;
using System.Diagnostics;

namespace PhoneShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly AppDbContext context;

        public OrderController(ILogger<OrderController> logger, AppDbContext context)
        {
            _logger = logger;
            this.context = context;
        }


        public IActionResult Index()
        {
            return View();
        }   
        
            
    
    }
}
