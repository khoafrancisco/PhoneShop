using PhoneShop.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PhoneShop.Models.Entities;


var builder = WebApplication.CreateBuilder(args);

// Cấu hình dịch vụ DbContext cho ứng dụng
builder.Services.AddDbContext<AppDbContext>(options =>
    // Sử dụng SQL Server làm cơ sở dữ liệu với chuỗi kết nối được lấy từ cấu hình
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {

        options.LoginPath = "/Admin/User/Login";
        options.AccessDeniedPath = "/Admin/User/AccessDenied";
        options.LogoutPath = "/Admin/User/Logout";

    });



    builder.Services.AddDistributedMemoryCache(); // Để sử dụng session trong bộ nhớ tạm
    builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian timeout của session
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });
    
    builder.Services.AddControllersWithViews();



    builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=User}/{action=Index}/{id?}");


    
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

    app.UseSession(); // Thêm dòng này để bật session


app.Run();
