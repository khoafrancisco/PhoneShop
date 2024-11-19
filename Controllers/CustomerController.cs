using Microsoft.AspNetCore.Mvc;
using PhoneShop.Models.Entities;
using PhoneShop.Models.ViewModels;
using PhoneShop.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;


namespace PhoneShop.Controllers
{
    public class CustomerController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public AuthenticationProperties? CustomerCookie { get; private set; }

        public CustomerController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // GET: Login
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginModel, string? returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                Customers? customer = _appDbContext.Customers.Where(u => u.Phone == loginModel.Phone && u.Password == loginModel.Password).FirstOrDefault();
                if (customer != null)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, customer.Phone!),
                    new Claim(ClaimTypes.Role, "Customer"),
                };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, "CustomerCookie");

                    var authProperties = new AuthenticationProperties
                    {
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                    };
                    await HttpContext.SignInAsync(
                        "CustomerCookie",
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);
                        Console.WriteLine("===="+returnUrl);
                    return LocalRedirect(returnUrl ?? "/Home/Index");
                }
                else
                {
                    ViewBag.Message = "Tài khoản không tồn tại";
                }
            }
            else
            {
                ViewBag.Message = "Không hợp lệ";
            }
            return View(loginModel);

        }

        // POST: Login
        [HttpPost]
        public async Task<IActionResult> LoginOld(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = await _appDbContext.Customers
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
        // POST: Register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem số điện thoại đã tồn tại chưa
                var existingCustomer = await _appDbContext.Customers
                    .FirstOrDefaultAsync(c => c.Phone == model.Phone);

                if (existingCustomer != null)
                {
                    ModelState.AddModelError("Phone", "Số điện thoại này đã được sử dụng.");
                    return View(model);
                }

                // Tạo một khách hàng mới
                var newCustomer = new Customers
                {
                    Phone = model.Phone,
                    Password = model.Password, // Cân nhắc mã hóa mật khẩu
                    CreatedDate = DateTime.Now
                };

                // Thêm khách hàng vào cơ sở dữ liệu
                _appDbContext.Customers.Add(newCustomer);
                await _appDbContext.SaveChangesAsync();

                // Lưu thông báo vào TempData
                TempData["SuccessMessage"] = "Tài khoản của bạn đã được tạo thành công. Vui lòng đăng nhập.";

                // Chuyển hướng đến trang đăng nhập sau khi đăng ký thành công
                return RedirectToAction("Login", "Customer");
            }

            // Nếu có lỗi, hiển thị lại form đăng ký
            return View(model);
        }

        // GET: Logout
        
    }
}
