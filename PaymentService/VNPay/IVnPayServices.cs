namespace PhoneShop.Models.Payment
{
    public interface IVnPayServices
    {
        string CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
        PaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}