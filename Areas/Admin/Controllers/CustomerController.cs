using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhoneShop.Models;
using PhoneShop.Models.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;

namespace PhoneShop.Admin.Controllers
{
    [Area("Admin")]
    // [Authorize(Roles = "Admin")]
    [Authorize(AuthenticationSchemes = "AdminCookie", Roles = "Admin")]
    public class CustomerController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(AppDbContext context, ILogger<CustomerController> logger)
        {
            _appDbContext = context;
            _logger = logger;
        }

        // GET: Admin/Customer
        public async Task<IActionResult> Index(string sortColumn, string sortOrder)
        {
            var customers = _appDbContext.Customers.AsQueryable();

            ViewData["FullNameSortOrder"] = sortColumn == "FullName" && sortOrder == "asc" ? "desc" : "asc";
            ViewData["ProvinceSortOrder"] = sortColumn == "Province" && sortOrder == "asc" ? "desc" : "asc";
            ViewData["DistrictSortOrder"] = sortColumn == "District" && sortOrder == "asc" ? "desc" : "asc";
            ViewData["WardSortOrder"] = sortColumn == "Ward" && sortOrder == "asc" ? "desc" : "asc";
            ViewData["SortColumn"] = sortColumn;
            ViewData["SortOrder"] = sortOrder;

            customers = sortColumn switch
            {
                "FullName" => sortOrder == "asc" ? customers.OrderBy(c => c.FullName) : customers.OrderByDescending(c => c.FullName),
                "Province" => sortOrder == "asc" ? customers.OrderBy(c => c.Province) : customers.OrderByDescending(c => c.Province),
                "District" => sortOrder == "asc" ? customers.OrderBy(c => c.District) : customers.OrderByDescending(c => c.District),
                "Ward" => sortOrder == "asc" ? customers.OrderBy(c => c.Ward) : customers.OrderByDescending(c => c.Ward),
                _ => customers.OrderBy(c => c.CustomerID)
            };

            return View(await customers.ToListAsync());
        }

        // GET: Admin/Customer/Detail/5
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            var customer = id.HasValue ? await _appDbContext.Customers.FindAsync(id) : new Customers
            {
                Email = string.Empty,
                Password = string.Empty,
                Phone = string.Empty,
                Province = string.Empty,
                District = string.Empty,
                Ward = string.Empty,
            };

            if (customer == null && id.HasValue)
            {
                return NotFound("Không tìm thấy khách hàng");
            }

            return View(customer);
        }

        // POST: Admin/Customer/Save
        [HttpPost]
        public async Task<IActionResult> Save(Customers customer)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Dữ liệu không hợp lệ" });
            }

            try
            {
                if (customer.CustomerID > 0)
                {
                    var existingCustomer = await _appDbContext.Customers.FindAsync(customer.CustomerID);
                    if (existingCustomer == null)
                    {
                        return Json(new { success = false, message = "Không tìm thấy khách hàng để cập nhật" });
                    }

                    existingCustomer.FullName = customer.FullName;
                    existingCustomer.Email = customer.Email;
                    existingCustomer.Phone = customer.Phone;
                    existingCustomer.Note = customer.Note;
                    existingCustomer.DeliveryMethod = customer.DeliveryMethod;
                    existingCustomer.Province = customer.Province;
                    existingCustomer.District = customer.District;
                    existingCustomer.Ward = customer.Ward;
                    existingCustomer.PaymentMethod = customer.PaymentMethod;
                    existingCustomer.TotalAmount = customer.TotalAmount;
                    existingCustomer.Password = customer.Password;

                    _appDbContext.Customers.Update(existingCustomer);
                }
                else
                {
                    customer.CreatedDate = DateTime.Now;
                    await _appDbContext.Customers.AddAsync(customer);
                }

                await _appDbContext.SaveChangesAsync();
                return Json(new { success = true, message = "Lưu khách hàng thành công" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Có lỗi xảy ra khi lưu khách hàng");
                return Json(new { success = false, message = "Có lỗi xảy ra khi lưu khách hàng" });
            }
        }

        // DELETE: Admin/Customer/Delete/5
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _appDbContext.Customers.FindAsync(id);
            if (customer == null)
            {
                return Json(new { success = false, message = "Không tìm thấy khách hàng" });
            }

            try
            {
                _appDbContext.Customers.Remove(customer);
                await _appDbContext.SaveChangesAsync();
                return Json(new { success = true, message = "Xóa khách hàng thành công" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Có lỗi xảy ra khi xóa khách hàng");
                return Json(new { success = false, message = "Có lỗi xảy ra khi xóa khách hàng" });
            }
        }
    }
}
