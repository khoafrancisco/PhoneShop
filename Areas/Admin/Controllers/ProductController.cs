using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhoneShop.Models;
using PhoneShop.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneShop.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<ProductController> _logger;

        public ProductController(AppDbContext context, ILogger<ProductController> logger)
        {
            _appDbContext = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            // Fetch the list of products asynchronously
            var products = await _appDbContext.Products.ToListAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            // Retrieve the product and associated media, or initialize a new product
            var product = id.HasValue ? await _appDbContext.Products.FindAsync(id) : new Products();
            var mediaList = await _appDbContext.Medias.Where(m => m.ProductID == id).ToListAsync();
            ViewBag.MediaList = mediaList ?? new List<Media>();

            return PartialView("~/Areas/Admin/Views/Product/Detail.cshtml", product);
        }

        [HttpPost]
        public async Task<IActionResult> Save(Products product, List<Media> mediaList)
        {
            try
            {
                if (product.ProductID > 0)
                {
                    // Update existing product
                    _appDbContext.Products.Update(product);
                }
                else
                {
                    // Add new product
                    await _appDbContext.Products.AddAsync(product);
                }
                await _appDbContext.SaveChangesAsync();

                // Save media for product
                foreach (var media in mediaList)
                {
                    media.ProductID = product.ProductID;

                    if (media.IsMain ?? false)
                    {
                        // Ensure only one main image per product
                        var existingMain = await _appDbContext.Medias
                            .FirstOrDefaultAsync(m => m.ProductID == product.ProductID && (m.IsMain ?? false));
                        if (existingMain != null)
                        {
                            existingMain.IsMain = false;
                            _appDbContext.Medias.Update(existingMain);
                        }
                    }

                    await _appDbContext.Medias.AddAsync(media);
                }

                await _appDbContext.SaveChangesAsync();
                return Json(new { success = true, message = "Lưu sản phẩm thành công" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving product");
                return Json(new { success = false, message = "Có lỗi xảy ra" });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _appDbContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound("Không tìm thấy sản phẩm");
            }

            // Remove related media before deleting the product
            var mediaList = await _appDbContext.Medias.Where(m => m.ProductID == id).ToListAsync();
            _appDbContext.Medias.RemoveRange(mediaList);

            _appDbContext.Products.Remove(product);
            await _appDbContext.SaveChangesAsync();

            return Json(new { success = true, message = "Xóa sản phẩm thành công" });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMedia(int id)
        {
            // Remove a single media item
            var media = await _appDbContext.Medias.FindAsync(id);
            if (media == null)
            {
                return NotFound("Không tìm thấy ảnh");
            }

            _appDbContext.Medias.Remove(media);
            await _appDbContext.SaveChangesAsync();

            return Json(new { success = true, message = "Xóa ảnh thành công" });
        }
    }
}
