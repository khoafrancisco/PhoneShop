// using Microsoft.AspNetCore.Mvc;
// using PhoneShop.Models;
// using Microsoft.AspNetCore.Authorization;
// using PhoneShop.Models.Entities;
// using System.Security.Claims;
// using Microsoft.AspNetCore.Authentication.Cookies;
// using Microsoft.AspNetCore.Authentication;

// namespace PhoneShop.Controllers;

// public class CustomersController : Controller {
//     private readonly ILogger<CustomersController> _logger;
//     private readonly AppDbContext _appDbContext;

//     public CustomersController(ILogger<CustomersController> logger, AppDbContext context) {
//         _logger = logger;
//         _appDbContext = context;
//     }
//     public IActionResult Index() {
//         return View();
//     }
//     public IActionResult Create() {
//         return View();
//     }
//     [HttpPost]
//     [ValidateAntiForgeryToken]
//     public async Task<IActionResult> Create(Customers model) {
//         if (!ModelState.IsValid) {
//             return View(model);
//         }
//         _appDbContext.Customers.Add(model);
//         await _appDbContext.SaveChangesAsync();
//         return RedirectToAction("Index");
//     }
//     public IActionResult Edit(int id) {
//         var customer = _appDbContext.Customers.Find(id);
//         return View(customer);
//     }
//     [HttpPost]
//     [ValidateAntiForgeryToken]
//     public async Task<IActionResult> Edit(Customers model) {
//         if (!ModelState.IsValid) {
//             return View(model);
//         }
//         _appDbContext.Customers.Update(model);
//         await _appDbContext.SaveChangesAsync();
//         return RedirectToAction("Index");
//     }
//     public IActionResult Delete(int id) {
//         var customer = _appDbContext.Customers.Find(id);
//         return View(customer);
//     }
//     [HttpPost]
//     [ValidateAntiForgeryToken]
//     public async Task<IActionResult> Delete(Customers model) {
//         if (!ModelState.IsValid) {
//             return View(model);
//         }
//         _appDbContext.Customers.Remove(model);
//         await _appDbContext.SaveChangesAsync();
//         return RedirectToAction("Index");
//     }
//     public IActionResult Details(int id) {
//         var customer = _appDbContext.Customers.Find(id);
//         return View(customer);
//     }

// }

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneShop.Models;

public class CustomersController : Controller
{
    private readonly AppDbContext _context;
    public CustomersController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        // Lấy danh sách khách hàng từ cơ sở dữ liệu
        var customers = _context.Customers.ToList();

        // In dữ liệu ra console
        foreach (var customer in customers)
        {
            Console.WriteLine($"CustomerID: {customer.CustomerID}, FullName: {customer.FullName}, Email: {customer.Email}, Phone: {customer.Phone}, Address: {customer.Address}, CreatedDate: {customer.CreatedDate}");
        }

        // Trả về view hoặc dữ liệu
        return View(customers);
    }
}
