// using Microsoft.AspNetCore.Mvc;
// using System.Collections.Generic;
// using System.Linq;

// public class CartController : Controller
// {
//     public IActionResult AddToCart(int productId, string productName, decimal price)
//     {
//         var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

//         var existingItem = cart.FirstOrDefault(x => x.ProductID == productId);
//         if (existingItem != null)
//         {
//             existingItem.Quantity++;
//         }
//         else
//         {
//             cart.Add(new CartItem
//             {
//                 ProductID = productId,
//                 Name = productName,
//                 Quantity = 1,
//                 Price = price
//             });
//         }

//         HttpContext.Session.SetObjectAsJson("Cart", cart);
//         return RedirectToAction("Index");
//     }

//     public IActionResult Index()
//     {
//         var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
//         return View(cart);
//     }

//     public IActionResult RemoveFromCart(int productId)
//     {
//         var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

//         var itemToRemove = cart.FirstOrDefault(x => x.ProductID == productId);
//         if (itemToRemove != null)
//         {
//             cart.Remove(itemToRemove);
//             HttpContext.Session.SetObjectAsJson("Cart", cart);
//         }

//         return RedirectToAction("Index");
//     }
// }
