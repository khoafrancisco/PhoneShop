using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneShop.Models.Entities;

[Table("Media")]
public class Media
{
    [Key]
    public int MediaID { get; set; }
    public int ProductID { get; set; }
    public string? MediaURL { get; set; }
    public bool? IsMain { get; set; }   
}
