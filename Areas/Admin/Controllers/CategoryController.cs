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
    public class CategoryController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(AppDbContext context, ILogger<CategoryController> logger)
        {
            _appDbContext = context;
            _logger = logger;
        }

        // GET: Admin/Category
        public async Task<IActionResult> Index()
        {
            var categories = await _appDbContext.Categories.ToListAsync();
            return View(categories);
        }

        // GET: Admin/Category/Detail/5
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            // Check if an id is provided, and retrieve the category if it exists; otherwise, initialize a new category.
            var category = id.HasValue ? await _appDbContext.Categories.FindAsync(id) : new Category { CategoryName = "Default Name" };

            if (category == null && id.HasValue)
            {
                // If id is provided but the category is not found, return a 404 error.
                return NotFound("Không tìm thấy danh mục");
            }

            return View(category);
        }

        // POST: Admin/Category/Save
        [HttpPost]
        public async Task<IActionResult> Save(Category category)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid data" });
            }

            try
            {
                if (category.CategoryID > 0)
                {
                    // Update existing category
                    _appDbContext.Categories.Update(category);
                }
                else
                {
                    // Add new category
                    await _appDbContext.Categories.AddAsync(category);
                }

                await _appDbContext.SaveChangesAsync();
                return Json(new { success = true, message = "Lưu danh mục thành công" });
            }
            catch
            {
                return Json(new { success = false, message = "Có lỗi xảy ra khi lưu danh mục" });
            }
        }

        // DELETE: Admin/Category/Delete/5
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _appDbContext.Categories.FindAsync(id);
            if (category == null)
            {
                return Json(new { success = false, message = "Không tìm thấy danh mục" });
            }

            try
            {
                _appDbContext.Categories.Remove(category);
                await _appDbContext.SaveChangesAsync();
                return Json(new { success = true, message = "Xóa danh mục thành công" });
            }
            catch
            {
                return Json(new { success = false, message = "Có lỗi xảy ra khi xóa danh mục" });
            }
        }
    }
}
