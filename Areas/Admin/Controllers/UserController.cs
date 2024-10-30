using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PhoneShop.Models;
using Microsoft.AspNetCore.Authorization;
using PhoneShop.Models.Entities;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Identity.Client;
using System.Text.Json;
using PhoneShop.Admin.Models;

namespace PhoneShop.Admin.Controllers;

[Area("Admin")]
public class UserController : Controller
{
private readonly ILogger<UserController> _logger;
    private readonly AppDbContext _appDbContext;

    public UserController(ILogger<UserController> logger, AppDbContext context)
    {
        _logger = logger;
        _appDbContext = context;
    }
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginModel loginModel, string? returnUrl = null)
    {
        if (ModelState.IsValid)
        {
            Users? user = _appDbContext.Users.Where(u => u.UserName == loginModel.UserName && u.PasswordHash == loginModel.Password).FirstOrDefault();
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(ClaimTypes.Role, user.Role!),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.

                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(5),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    //IsPersistent = true,
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                return LocalRedirect(returnUrl ?? "/Admin/User/Index");
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

    public IActionResult AccessDenied()
    {
        return View();
    }
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "User");
    }
    [Authorize(Roles = "Admin")]
    public IActionResult Index()
    {
        List<Users> users = _appDbContext.Users.ToList();
        return View(users);
    }
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult Detail()
    {
        return PartialView("~/Areas/Admin/Views/Partial/User/Detail.cshtml");
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult Save(Users user)
    {
        try
        {
            user.CreatedDate = DateTime.Now;
            if (user.UserID > 0)
            {
                _appDbContext.Users.Update(user);
                _appDbContext.SaveChanges();
                return Json(new { success = true, message = "Cập nhật thành công" });
            }
            else
            {
                _appDbContext.Users.Add(user);
                _appDbContext.SaveChanges();
                return Json(new { success = true, message = "Thêm thành công" });
            }
        }
        catch
        {
            return Json(new { success = false, message = "Có lỗi xảy ra" });
        }
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Edit(int id)
    {
        Users? users = _appDbContext.Users.Where(x => x.UserID == id).FirstOrDefault();
        if (users == null)
        {
            return NotFound("Không tìm thấy người dùng");
        }
        else
        {
            _logger.LogInformation(users.PasswordHash);
            return PartialView("~/Areas/Admin/Views/Partial/User/Detail.cshtml", users);
        }
    }
    [Authorize(Roles = "Admin")]

    [HttpPost]
    public async Task<IActionResult> Edit(Users users)
    {
        if (ModelState.IsValid)
        {
            _logger.LogInformation(users.UserID.ToString());
            Users? oldUser = _appDbContext.Users.Where(x => x.UserID == users.UserID).FirstOrDefault();
            if (oldUser == null)
            {
                return NotFound("Không tìm thấy người dùng");
            }
            else
            {
                oldUser.UserName = users.UserName;
                oldUser.PasswordHash = users.PasswordHash;
                oldUser.Role = users.Role;
                _appDbContext.Users.Update(oldUser);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

        }
        return View(users);
    }
    [Authorize(Roles = "Admin")]
    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(int id)
    {

        Users? users = _appDbContext.Users.Where(x => x.UserID == id).FirstOrDefault();
        if (users == null)
        {
            return NotFound("Không tìm thấy người dùng");
        }
        else
        {
            _appDbContext.Users.Remove(users);
            await _appDbContext.SaveChangesAsync();
            return Json(new { success = true, message = "Xóa thành công" });
        }
    }
}
