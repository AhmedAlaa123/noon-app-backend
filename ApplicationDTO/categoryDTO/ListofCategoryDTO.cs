using noone.CustomAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace noone.ApplicationDTO.categoryDTO
{
    public class ListofCategoryDTO
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required, MinLength(3), uniqeCategoryName]
        public string Name { get; set; }

       public List<string> productsname { get; set; } = new List<string>();

        public List<string> subCategoryName { get; set; } = new List<string>();
    }
}
