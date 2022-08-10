using noone.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace noone.ApplicationDTO.categoryDTO
{
    public class CategoryCreateDTO
    {
        

        [Required, MinLength(3), uniqeCategoryName]
        public string Name { get; set; }
    }
}
