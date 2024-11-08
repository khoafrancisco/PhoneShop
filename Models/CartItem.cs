using PhoneShop.Models.Entities;

public class CartItem
{
    public Products products { get; set; } = new Products();
    public int Quantity { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime? CreatedDate { get; set; }
}


public class Cart
{
    public List<CartItem> Items { get; set; } = new List<CartItem>();

    public decimal TotalPrice => Items.Sum(i => i.TotalAmount * i.Quantity);

    public void AddItem(CartItem item)
    {
        var existingItem = Items.FirstOrDefault(i => i.products.ProductID == item.products.ProductID);
        if (existingItem != null)
        {
            existingItem.Quantity += item.Quantity;
        }
        else
        {
            Items.Add(item);
        }
    }

    public void RemoveItem(int productId)
    {
        var item = Items.FirstOrDefault(i => i.products.ProductID == productId);
        if (item != null)
        {
            Items.Remove(item);
        }
    }
}