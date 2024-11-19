// Định nghĩa của OrderDetailViewModel
public class OrderDetailViewModel
{
    public int OrderDetailID { get; set; }    // ID chi tiết của đơn hàng
    public int OrderID { get; set; }           // ID của đơn hàng
    public int ProductID { get; set; }         // ID sản phẩm
    public string? Name { get; set; }          // Tên sản phẩm (nếu cần hiển thị)
    public int Quantity { get; set; }          // Số lượng sản phẩm
    public decimal UnitPrice { get; set; }     // Đơn giá sản phẩm
    public decimal TotalPrice => Quantity * UnitPrice; // Tổng giá (Quantity * UnitPrice)
}