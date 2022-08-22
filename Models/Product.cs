using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace noone.Models
{
    [Table(nameof(Product))]
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required, MinLength(3)]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }
        public string Description { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [ForeignKey("Company")]
        public Guid? CompanyId { get; set; }
        public Company Company { get; set; }

        [ForeignKey("Category")]
        public Guid Category_Id { get; set; }
        public Category Category { get; set; }

        [ForeignKey("SucCategory")]
        public Guid SucCategory_Id { get; set; }

        public SubCategory SucCategory { get; set; }

        public  string Image { get; set; }
       // public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
        public ICollection<ProductOrder> ProductOrders { get; set; }
        public ICollection<UserProductRate> UserProductRates { get; set; }

        [InverseProperty("OriginalProduct")]
        public ICollection<RelatedSaledProduct> RelatedSaledProductOriginal { get; set; }

        [InverseProperty("RelatedProduct")]
        public ICollection<RelatedSaledProduct> RelatedSaledProductRelatedProduct { get; set; }

    }
}
