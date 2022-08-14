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

    
    }
}
