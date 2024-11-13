using Microsoft.AspNetCore.Mvc;
using PhoneShop.Models.Entities;
using PhoneShop.Models.ViewModels;
using PhoneShop.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PhoneShop.Controllers
{
    public class CustomerController : Controller
    {
        private readonly AppDbContext _context;

        public CustomerController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = await _context.Customers
                    .Where(c => c.Phone == model.Phone && c.Password == model.Password)
                    .FirstOrDefaultAsync();

                if (customer != null)
                {
                    // Handle login (e.g., create session)
                    return RedirectToAction("Index", "Customer");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Số điện thoại hoặc mật khẩu không đúng");
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

        // POST: Register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if the phone number already exists
                var existingCustomer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.Phone == model.Phone);

                if (existingCustomer != null)
                {
                    ModelState.AddModelError("Phone", "Số điện thoại này đã được sử dụng.");
                    return View(model);
                }

                // Create a new customer entity
                var newCustomer = new Customers
                {
                    Phone = model.Phone,
                    Password = model.Password, // Consider hashing the password
                    CreatedDate = DateTime.Now
                };

                // Add the new customer to the database
                _context.Customers.Add(newCustomer);
                await _context.SaveChangesAsync();

                // Redirect to the login page after successful registration
                return RedirectToAction("Login", "Customer");
            }

            // If we reach here, something went wrong, redisplay the form
            return View(model);
        }

        // GET: Logout
        public IActionResult Logout()
        {
            // Handle logout (e.g., clear session)
            return RedirectToAction("Index", "Home");
        }
    }
}
