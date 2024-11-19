using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhoneShop.Models;
using PhoneShop.Models.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;

namespace PhoneShop.Admin.Controllers
{
    [Area("Admin")]
    // [Authorize(Roles = "Admin")]
[Authorize(AuthenticationSchemes = "AdminCookie", Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<ProductController> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProductController(AppDbContext context, ILogger<ProductController> logger, IWebHostEnvironment hostingEnvironment)
        {
            _appDbContext = context;
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Admin/Product
        public async Task<IActionResult> Index(string sortOrder, int ?CategoryID)
        {
            var products = _appDbContext.Products.Include(p => p.Category).AsQueryable();

            if (CategoryID.HasValue)
            {
                products = products.Where(p => p.CategoryID == CategoryID.Value);
            }
            products = sortOrder switch
            {
                "name_desc" => products.OrderByDescending(p => p.Name),
                "price" => products.OrderBy(p => p.Price),
                "price_desc" => products.OrderByDescending(p => p.Price),
                "stock" => products.OrderBy(p => p.Stock),
                "stock_desc" => products.OrderByDescending(p => p.Stock),
                _ => products.OrderBy(p => p.Name),
            };
            ViewData["Categories"] = new SelectList(await _appDbContext.Categories.ToListAsync(), "CategoryID", "CategoryName");
            ViewData["SortOrder"] = sortOrder;

            return View(await products.ToListAsync());
        }

        // GET: Admin/Product/Detail/5
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            ViewData["Categories"] = _appDbContext.Categories
                .Select(c => new SelectListItem(c.CategoryName, c.CategoryID.ToString()))
                .ToList();
            ViewData["Medias"] = await _appDbContext.Medias.Where(m => m.ProductID == id).ToListAsync();

            var product = id.HasValue ? await _appDbContext.Products.FindAsync(id) : new Products { Name = "Default Product", CreatedDate = DateTime.Now };

            if (product == null && id.HasValue)
            {
                return NotFound("Không tìm thấy sản phẩm");
            }

            return View(product);
        }

        // POST: Admin/Product/Save
        [HttpPost]
        public async Task<IActionResult> Save(Products product, IFormFile? MainImage, IFormFileCollection? AdditionalImages)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Dữ liệu không hợp lệ" });
            }

            try
            {
                Products? oldProduct = null;
                if (product.ProductID > 0)
                {
                    oldProduct = await _appDbContext.Products.FindAsync(product.ProductID);
                    if (oldProduct == null)
                    {
                        return Json(new { success = false, message = "Không tìm thấy sản phẩm" });
                    }
                }

                // Xử lý ảnh chính
                if (MainImage != null && MainImage.Length > 0)
                {
                    if (oldProduct != null && !string.IsNullOrEmpty(oldProduct.Image))
                    {
                        var oldImagePath = Path.Combine(_hostingEnvironment.WebRootPath, "img/products", oldProduct.Image);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    var mainImagePath = Path.Combine(_hostingEnvironment.WebRootPath, "img/products", MainImage.FileName);
                    Directory.CreateDirectory(Path.GetDirectoryName(mainImagePath)!);
                    using (var stream = new FileStream(mainImagePath, FileMode.Create))
                    {
                        await MainImage.CopyToAsync(stream);
                    }
                    product.Image = MainImage.FileName;
                }

                // Xử lý ảnh phụ
                if (AdditionalImages != null && AdditionalImages.Any())
                {
                    if (oldProduct != null)
                    {
                        foreach (var media in _appDbContext.Medias.Where(m => m.ProductID == oldProduct.ProductID))
                        {
                            var oldMediaPath = Path.Combine(_hostingEnvironment.WebRootPath, "img/products", media.MediaURL!);
                            if (System.IO.File.Exists(oldMediaPath))
                            {
                                System.IO.File.Delete(oldMediaPath);
                            }
                            _appDbContext.Medias.Remove(media);
                        }
                    }

                    foreach (var image in AdditionalImages)
                    {
                        if (image != null && image.Length > 0)
                        {
                            var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "img/products", image.FileName);
                            Directory.CreateDirectory(Path.GetDirectoryName(imagePath)!);
                            using (var stream = new FileStream(imagePath, FileMode.Create))
                            {
                                await image.CopyToAsync(stream);
                            }

                            var media = new Media
                            {
                                ProductID = product.ProductID,
                                MediaURL = image.FileName
                            };
                            await _appDbContext.Medias.AddAsync(media);
                        }
                    }
                }

                if (oldProduct != null)
                {
                    oldProduct.Name = product.Name;
                    oldProduct.Description = product.Description;
                    oldProduct.Price = product.Price;
                    oldProduct.Stock = product.Stock;
                    oldProduct.CategoryID = product.CategoryID;
                    if (!string.IsNullOrEmpty(product.Image))
                    {
                        oldProduct.Image = product.Image;
                    }
                    _appDbContext.Products.Update(oldProduct);
                }
                else
                {
                    product.CreatedDate = DateTime.Now;
                    await _appDbContext.Products.AddAsync(product);
                }

                await _appDbContext.SaveChangesAsync();
                return Json(new { success = true, message = "Lưu sản phẩm thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Lỗi: {ex.Message}" });
            }
        }

        // DELETE: Admin/Product/Delete/5
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _appDbContext.Products.FindAsync(id);
            if (product == null)
            {
                return Json(new { success = false, message = "Không tìm thấy sản phẩm" });
            }

            try
            {
                _appDbContext.Products.Remove(product);
                await _appDbContext.SaveChangesAsync();
                return Json(new { success = true, message = "Xóa sản phẩm thành công" });
            }
            catch
            {
                return Json(new { success = false, message = "Có lỗi xảy ra khi xóa sản phẩm" });
            }
        }
    }
}
