using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace noone.Models
{
    [Table(nameof(SubCategory))]
    public class SubCategory
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required, MinLength(3)]
        public string Name { get; set; }

        public string Image { get; set; }

        [ForeignKey("Category")]
        public Guid? Category_Id { get; set; }
        public Category? Category { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
