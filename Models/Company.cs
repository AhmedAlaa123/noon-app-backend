using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace noone.Models
{
    [Table("Company")]
    public class Company
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required,MinLength(3)]
        public string Name { get; set; }

        [Required]
        public string BrandImage { get; set; }

        [Required,MinLength(11),RegularExpression("^[0-9]{11}$")]
        public string ContactNumber { get; set; }

        // Products That are belong to this company
        public ICollection<Product> Products { get; set; }


    }
}
