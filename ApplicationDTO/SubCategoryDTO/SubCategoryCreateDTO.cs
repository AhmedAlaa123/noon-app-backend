using System.ComponentModel.DataAnnotations;
using noone.CustomAttributes;

namespace noone.ApplicationDTO.SubCategoryDto
{
    public class SubCategoryCreateDTO
    {
        [Required(ErrorMessage = "من فضلك ادخل اسم الفئة"),UniqSupCategoryName]
        [MinLength(3, ErrorMessage = "يجب ان يكون الاسم اكثر من حرفين")]
        public string Name { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل الصورة")]
        public Guid Category_Id { get; set; }
        public IFormFile Image { get; set; }
    }
}
