using System.ComponentModel.DataAnnotations;

namespace Common.Entities;
public class PaymentMethod
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [EmailAddress]
    public string Email { get; set; }

}