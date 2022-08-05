using System.ComponentModel.DataAnnotations;

namespace noone.ApplicationDTO.ApplicationUserDTO
{
    public class ApplicationUserSignInDTO
    {
        [Required(ErrorMessage = "ادخل اسم المستخدم *")]
        [Display(Name = "اسم المستخدم : ")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "ادخل كلمه المرور *")]
        [Display(Name = "كلمه المرور :")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

       
    }
}
