using Microsoft.AspNetCore.Http;
using System.Text.Json;

public static class SessionExtensions
{
    public static void SetCart(this ISession session, Cart cart)
    {
        var cartData = JsonSerializer.Serialize(cart); // Sử dụng JsonSerializer để chuyển đổi đối tượng thành chuỗi JSON
        session.SetString("Cart", cartData);
    }

    public static Cart? GetCart(this ISession session)
    {
        var cartData = session.GetString("Cart");
        return string.IsNullOrEmpty(cartData) ? new Cart() : JsonSerializer.Deserialize<Cart>(cartData);
    }
}
