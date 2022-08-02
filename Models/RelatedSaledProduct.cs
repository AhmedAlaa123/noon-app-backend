using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace noone.Models
{
    [Table(nameof(RelatedSaledProduct))]
    public class RelatedSaledProduct
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        [ForeignKey("OriginalProduct")]
        public Guid OriginalProductId { get; set; }

        [ForeignKey("RelatedProduct")]
        public Guid RelatedProductId { get; set; }

        public Product OriginalProduct { get; set; }
        public Product RelatedProduct { get; set; }


    }
}
