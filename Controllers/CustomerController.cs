using Microsoft.AspNetCore.Mvc;
using PhoneShop.Models.Entities;
using PhoneShop.Models.ViewModels;
using PhoneShop.Models;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace PhoneShop.Controllers
{
    public class CustomerController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly AppDbContext _context;

        public CustomerController(ILogger<CustomerController> logger, AppDbContext context)
        {
            _context = context;
            _appDbContext = context;
        }

    // GET: Login
    public IActionResult Login()
    {
        return View();
    }   
    public IActionResult Checkout()
    {
        return View();
    }
    // POST: Login
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var customer = await _appDbContext.Customers
                .Where(c => c.Email == model.Email && c.Password == model.Password)
                .FirstOrDefaultAsync();

            if (customer != null)
            {
                // Xử lý đăng nhập (ví dụ: tạo session)
                return RedirectToAction("Checkout", "Customer");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Email hoặc mật khẩu không đúng");
            }
        }

        // If we get to this point, something went wrong, redisplay the form
        return View(model);
    }
    

    // GET: Register
    public IActionResult Register()
    {
        return View();
    }
    // GET: 
    // GET: Logout
        public IActionResult Logout()
        {
            // Xử lý đăng xuất (ví dụ: xóa session)
            return RedirectToAction("Index", "Home");
        }
    }
}


        