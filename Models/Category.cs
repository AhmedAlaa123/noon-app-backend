using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace noone.Models
{
    [Table(nameof(Category))]
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid  Id { get; set; }

        [Required,MinLength(3)]
        public string Name { get; set; }

        // products That Belong To This Category
        public ICollection<Product> Products { get; set; }

        // Sub Categories That  Belong To This Category
        public ICollection<SubCategory> SubCategories { get; set; }


    }
}
