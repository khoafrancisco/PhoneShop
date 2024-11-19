using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneShop.Models;
using PhoneShop.Models.Payment;

[Authorize(AuthenticationSchemes = "CustomerCookie", Roles = "Customer")]
public class CheckoutController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IVnPayServices _vnPayService;

    public CheckoutController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IVnPayServices vnPayService)
    {
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
        _vnPayService = vnPayService;
    }

    public IActionResult Index(OrderViewModel model)
    {
        return View(model);
    }
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

    [HttpPost]
    public IActionResult CreatePaymentUrlVnpay(PaymentInformationModel model)
    {
        var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

        return Redirect(url);
    }
    [HttpGet]
    public IActionResult PaymentCallbackVnpay()
    {
        var response = _vnPayService.PaymentExecute(Request.Query);

        return View(response);
    }


}
