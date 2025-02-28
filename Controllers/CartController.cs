using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneShop.Models;
using PhoneShop.Models.Entities; // Sửa lại cho đúng namespace của bạn
using System;
using System.Linq;


public class CartController : Controller
{
    private readonly AppDbContext _context;

    public CartController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var cart = HttpContext.Session.GetCart();
        return View(cart);
    }

    [HttpPost]
    public IActionResult AddToCart(int productId, int quantity = 1)
    {
        var cart = HttpContext.Session.GetCart() ?? new Cart();

        var product = _context.Products.FirstOrDefault(p => p.ProductID == productId);
        if (product != null)
        {
            var item = new CartItem
            {
                products = product,
                Quantity = quantity,
                TotalAmount = product.Price * quantity,
                CreatedDate = DateTime.Now
            };

            cart.AddItem(item);
            HttpContext.Session.SetCart(cart);
            return RedirectToAction("Index");
        }
        else
        {
            return NotFound();
        }

    }

    [HttpPost]
    public IActionResult RemoveFromCart(int productId)
    {
        var cart = HttpContext.Session.GetCart();
        if (cart != null)
        {
            cart.RemoveItem(productId);
            HttpContext.Session.SetCart(cart);
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public JsonResult UpdateQuantity(int productId, int quantity)
    {
        var cart = HttpContext.Session.GetCart();
        if (cart == null || cart.Items == null)
        {
            return Json(new { itemTotal = 0, cartTotal = 0 });
        }

        var item = cart.Items.FirstOrDefault(i => i.products.ProductID == productId);
        if (item != null)
        {
            item.Quantity = quantity;
            item.TotalAmount = item.products.Price * quantity; // Update total amount for item
            HttpContext.Session.SetCart(cart);

            // Return JSON values including total amount of the item and total amount of the cart
            var itemTotal = item.TotalAmount;
            var cartTotal = cart.Items.Sum(i => i.TotalAmount);

            return Json(new { itemTotal, cartTotal });
        }

        return Json(new { itemTotal = 0, cartTotal = 0 });
    }
}