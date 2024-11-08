// using Microsoft.AspNetCore.Mvc;
// using PhoneShop.Models;

// namespace PhoneShop.Controllers
// {
//     public class ReviewsController : Controller
//     {
//         private readonly AppDbContext _context;

//         private readonly DbContextOptions<AppDbContext> _options;

//         public ReviewsController(DbContextOptions<AppDbContext> options)
//         {
//             _options = options;
//             _context = new AppDbContext(_options);
//         }

//         // GET: Reviews for a specific product
//         public ActionResult Index(int productId)
//         {
//             var reviews = _context.Reviews.Where(r => r.ProductID == productId).ToList();
//             ViewBag.ProductID = productId; // For use in the view
//             return View(reviews);
//         }

//         // POST: Add a new review
//         [HttpPost]
//         public ActionResult Create(Review review)
//         {
//             if (ModelState.IsValid)
//             {
//                 review.CreatedDate = DateTime.Now;
//                 _context.Reviews.Add(review);
//                 _context.SaveChanges();
//                 return RedirectToAction("Index", new { productId = review.ProductID });
//             }
//             return View(review);
//         }
//     }
// }
