using Microsoft.AspNetCore.Mvc;
using PhoneShop.Models.Entities;
using PhoneShop.Models.ViewModels;
using PhoneShop.Models;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace PhoneShop.Controllers
{
    public class CustomerController : Controller
    {
        private readonly AppDbContext _context;

        public CustomerController(AppDbContext context)
        {
            _context = context;
        }

        // Phương thức hiển thị trang với form đăng ký và đăng nhập
        [HttpGet]
        public IActionResult Index()
        {
            // Trả về view Index với hai ViewModel: RegisterViewModel và LoginViewModel
            return View(new Tuple<RegisterViewModel, LoginViewModel>(new RegisterViewModel(), new LoginViewModel()));
        }

        // Xử lý form đăng ký
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem email đã tồn tại chưa
                var existingCustomer = _context.Customers.FirstOrDefault(c => c.Email == model.Email);
                if (existingCustomer != null)
                {
                    ModelState.AddModelError("Email", "Email này đã được sử dụng.");
                    // Trả về trang với thông tin lỗi và giữ nguyên thông tin đăng ký
                    return View("Index", new Tuple<RegisterViewModel, LoginViewModel>(model, new LoginViewModel()));
                }

                // Tạo khách hàng mới
                var customer = new Customers
                {
                    Email = model.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(model.Password), // Mã hóa mật khẩu
                    CreatedDate = DateTime.Now,
                    Phone = model.Phone,
                    Address = model.Address
                };

                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();

                // Thông báo đăng ký thành công
                ViewData["SuccessMessage"] = "Đăng ký thành công! Bạn có thể đăng nhập.";
                return View("Index", new Tuple<RegisterViewModel, LoginViewModel>(new RegisterViewModel(), new LoginViewModel()));
            }

            // Trả về trang với lỗi nếu model không hợp lệ
            return View("Index", new Tuple<RegisterViewModel, LoginViewModel>(model, new LoginViewModel()));
        }

        // Xử lý form đăng nhập
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = _context.Customers.FirstOrDefault(c => c.Email == model.Email);

                // Xác thực email và mật khẩu
                if (customer != null && BCrypt.Net.BCrypt.Verify(model.Password, customer.Password))
                {
                    // Đăng nhập thành công, chuyển đến trang chủ
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Email hoặc mật khẩu không đúng.");
            }

            // Trả về trang với lỗi nếu đăng nhập không thành công
            return View("Index", new Tuple<RegisterViewModel, LoginViewModel>(new RegisterViewModel(), model));
        }
    }
}
