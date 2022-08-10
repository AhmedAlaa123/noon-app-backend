using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace noone.Models
{
    [Table(nameof(DeliverCompany))]
    public class DeliverCompany
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required, MinLength(3)]
        public string Name { get; set; }

        [Required, StringLength(11), RegularExpression("^[0-9]{11}$")]
        public string ContactNumber { get; set; }

        [Required]
        public string Address { get; set; }


        // Orders That Delivery Company Will Deliver
        public ICollection<Order> Orders { get; set; }
    }
}
