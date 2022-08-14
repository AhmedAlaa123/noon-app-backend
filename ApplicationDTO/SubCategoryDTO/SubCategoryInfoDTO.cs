using System.ComponentModel.DataAnnotations;

namespace noone.ApplicationDTO.SubCategoryDto
{
    public class SubCategoryInfoDTO
    {

        public Guid SubCategoryId { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل اسم الفئة")]
        [MinLength(3, ErrorMessage = "يجب ان يكون الاسم اكثر من حرفين")]
        public string SubCategoryName { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل الصورة")]
        public string SubCategoryImage { get; set; }

    }
}
