using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneShop.Models.Entities;

[Table("Orders")]
public class Orders
{
    [Key]
    public int OrderID { get; set; }
    
    // Foreign key to Customer
    public int CustomerID { get; set; }
    
    // Navigation property to Customer
    public virtual Customers ? Customer { get; set; }  

    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string? PaymentStatus { get; set; }
    public string? ShippingAddress { get; set; }
    public decimal Discount { get; set; }

    // Navigation property for OrderDetails
    public virtual ICollection<OrderDetails>? OrderDetails { get; set; }
}
