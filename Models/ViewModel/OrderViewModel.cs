// Định nghĩa của OrderViewModel
public class OrderViewModel
{
    // Thông tin đơn hàng
    public int OrderID { get; set; }
    public int CustomerID { get; set; }
    public DateTime OrderDate { get; set; }
    public int TotalAmount { get; set; }
    public string PaymentStatus { get; set; } = string.Empty;
    public string ShippingAddress { get; set; } = string.Empty;
    public decimal Discount { get; set; }

    // Chi tiết trạng thái đơn hàng
    public string Status { get; set; } = string.Empty;
    public DateTime? UpdatedDate { get; set; }

    // Danh sách chi tiết đơn hàng
    public List<OrderDetailViewModel> OrderDetails { get; set; }

    // Constructor
    public OrderViewModel()
    {
        OrderDetails = new List<OrderDetailViewModel>();
    }
}