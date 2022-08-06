using System.ComponentModel.DataAnnotations;

namespace noone.ApplicationDTO.SubCategoryDTO
{
    public class SubCategoryUpdateDTO
    {
        [Required(ErrorMessage = "من فضلك ادخل اسم الفئة")]
        [MinLength(3, ErrorMessage = "يجب ان يكون الاسم اكثر من حرفين")]
        public string SubCategoryName { get; set; }

        public IFormFile? SubCategoryImage { get; set; }
    }
}
