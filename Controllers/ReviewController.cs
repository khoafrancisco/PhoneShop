using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhoneShop.Models;
using PhoneShop.Models.Entities;
using System;

namespace PhoneShop.Controllers
{
    [Route("review")]
    public class ReviewController : Controller
    {
        private readonly ILogger<ReviewController> _logger;
        private readonly AppDbContext _context;

        // Constructor
        public ReviewController(ILogger<ReviewController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost("add")]
        public IActionResult AddReview(int productId, string comment, int rating)
        {
            // Validate rating range
            if (rating < 1 || rating > 5)
            {
                ModelState.AddModelError("", "Rating must be between 1 and 5.");
            }

            if (ModelState.IsValid)
            {
                // Create a new review object
                var review = new Reviews
                {
                    ProductID = productId,
                    CustomerID = GetLoggedInCustomerId(), // Replace with actual logic to fetch logged-in customer ID
                    Rating = rating,
                    Comment = comment,
                    CreatedDate = DateTime.Now
                };

                // Save review to the database
                _context.Reviews.Add(review);
                _context.SaveChanges();

                // Redirect back to product details page
                return RedirectToAction("ProductDetails", "Products", new { productId });
            }

            // If invalid, return to product details with errors
            return RedirectToAction("ProductDetails", "Products", new { productId });
        }

        private int GetLoggedInCustomerId()
        {
            // Replace this with actual authentication logic (e.g., User.Identity.Name)
            return 1; // For now, using a hardcoded ID
        }
    }
}
