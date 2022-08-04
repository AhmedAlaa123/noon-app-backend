using noone.CustomAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace noone.DTO.categoryDTO
{
    public class AddcategoryDTO
    {
        

        [Required, MinLength(3), uniqeCategoryName]
        public string Name { get; set; }
    }
}
