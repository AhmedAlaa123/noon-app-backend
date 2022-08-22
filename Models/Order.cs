using noone.CustomAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace noone.Models
{
    [Table(nameof(Order))]
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataType(DataType.DateTime),Required, ChecKDate]
        public DateTime DeliverDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }

        // User That Has This Order
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [ForeignKey("DeliverCompany")]
        public Guid? DeliverCompany_Id { get; set; }
        public DeliverCompany DeliverCompany { get; set; }

        // Products in this order
        public ICollection<ProductOrder> ProductOrders { get; set; }

        public Bill Bill { get; set; }



    }
}
