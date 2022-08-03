using System.ComponentModel.DataAnnotations;

namespace noone.ApplicationDTO.SubCategoryDto
{
    public class SubCategoryCreateDTO
    {
        [Required(ErrorMessage = "من فضلك ادخل اسم الفئة")]
        [MinLength(3, ErrorMessage = "يجب ان يكون الاسم اكثر من حرفين")]
        public string Name { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل الصورة")]
        public string Image { get; set; }
        public Guid Category_Id { get; set; }
    }
}
