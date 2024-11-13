using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhoneShop.Models;
using PhoneShop.Models.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace PhoneShop.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<CustomerController> _logger;

        public OrderController(AppDbContext context, ILogger<CustomerController> logger)
        {
            _appDbContext = context;
            _logger = logger;
        }
        // GET: Admin/Order
        public async Task<IActionResult> Index()
        {
            var orders = await _appDbContext.Orders
                .Include(o => o.Customer)
                .ToListAsync();
            return View(orders);
        }    
    
    }
}