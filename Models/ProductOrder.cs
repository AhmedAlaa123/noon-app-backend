using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace noone.Models
{
    [Table(nameof(ProductOrder))]
    public class ProductOrder
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey("Product")]
        public Guid Product_Id { get; set; }
        public Product Product { get; set; }

        [ForeignKey("Order")]
        public Guid Order_Id { get; set; }
        public Order Order { get; set; }
    }
}
