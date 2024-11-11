using Microsoft.AspNetCore.Mvc;
using PhoneShop.Models;

public class CheckoutController : Controller
{
    [HttpPost]
    public IActionResult ProcessCheckout(CheckoutViewModel model)
    {
        if (model.PaymentMethod == "ZaloPay")
        {
            // Gọi phương thức thanh toán ZaloPay
            return RedirectToAction("ZaloPayPayment");
        }
        else if (model.PaymentMethod == "VNPay")
        {
            // Gọi phương thức thanh toán VNPay
            return RedirectToAction("VNPayPayment");
        }
        return View("Checkout");
    }

    public IActionResult ZaloPayPayment()
    {
        // Thêm logic xử lý thanh toán qua ZaloPay
        return View();
    }

    public IActionResult VNPayPayment()
    {
        // Thêm logic xử lý thanh toán qua VNPay
        return View();
    }
}
