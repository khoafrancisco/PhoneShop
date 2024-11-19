using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneShop.Models.Entities;

[Table("Customers")]
public class Customers
{
    [Key]
    public int CustomerID { get; set; }

    public string? FullName { get; set; } // Có thể null
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Phone { get; set; }

    [Column(TypeName = "nvarchar(MAX)")]
    public string? Note { get; set; }

    [Column(TypeName = "nvarchar(50)")]
    public string? DeliveryMethod { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public string? Province { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public string? District { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public string? Ward { get; set; }

    [Column(TypeName = "nvarchar(50)")]
    public string? PaymentMethod { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TotalAmount { get; set; }

    public DateTime CreatedDate { get; set; }

    public ICollection<Reviews> ? Reviews { get; set;}
}
