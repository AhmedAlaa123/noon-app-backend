using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace noone.Models
{
    [Table(nameof(ProductImage))]
    public class ProductImage
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string Image { get; set; }

        [ForeignKey("Product")]
        public Guid Product_Id { get; set; }
        public Product Product { get; set; }

    }
}
