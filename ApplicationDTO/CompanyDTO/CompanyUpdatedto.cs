using System.ComponentModel.DataAnnotations;

namespace noone.ApplicationDTO.CompanyDTO
{
    public class CompanyUpdatedto
    {
        [Required(ErrorMessage = "اسم الشركه مطلوب"), MinLength(3, ErrorMessage = "اقل عدد حروف 3")]
        public string Name { get; set; }

        [Required(ErrorMessage = "رقم الهاتف مطلوب"), StringLength(11, ErrorMessage = "رقم الهاتف يجب ان يكن 11 رقم"), RegularExpression("^[0-9]{11}$")]
        public string ContactNumber { get; set; }

        public IFormFile? BrandImage { get; set; }

    }
}
