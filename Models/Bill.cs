using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace noone.Models
{
    [Table(nameof(Bill))]
    public class Bill
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public double Price { get; set; }
        [ForeignKey("Order")]
        public Guid Order_Id { get; set; }
        public Order Order { get; set; }
    }
}
