using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Data;

namespace Common.Entities
{
    public class Subscription
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]

        //Nice thing that you have to specify SCHEMA FROM the damn SDK instead of it being included on a higher level.
        //This is why i dont like microslop.
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(20)]
        public string BillingCycle { get; set; } = "Monthly";

        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime? NextRenewalDate { get; set; }

        // category FK
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        // paymentmethod FK
        public int PaymentMethodId { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }

        //multitenant isolation
        [Required]
        public string UserId { get; set; } = string.Empty;
        public AppUser? User { get; set; }
    }
}