

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneShop.Models;

public class UsersController : Controller
{
    private readonly AppDbContext _context;
    public UsersController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        // Lấy danh sách khách hàng từ cơ sở dữ liệu
        var users = _context.Users.ToList();

        // Trả về view hoặc dữ liệu
        return View(users);
    }
    public IActionResult UsersDetails(int UserID) {
        var user = _context.Users.Find(UserID);
        return View(user);
    }
}
