using noone.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace noone.ApplicationDTO.ApplicationUserDTO
{
    public class ApplicationUserRegisterDTO
    {

        [Required(ErrorMessage ="البريد الالكترونى مطلوب*")]
        public string Email { get;set; }

        [Required(ErrorMessage ="اسم المستخدم مطلوب *"), UniqueUserName]
        public string UserName { get; set; }

        [Required(ErrorMessage ="كلمه المرور مطلوبه *"), MinLength(8,ErrorMessage ="كلمه المرور قصيره اقل عدد من الحروف 3 *")]
        public string Password { get; set; }

        [Required(ErrorMessage ="رقم الهاتف مطلوب"), StringLength(11,ErrorMessage ="رقم الهاتف ينبغى ان يكون 11 رقم *")]
        public  string PhoneNumber { get; set; }

        [Required(ErrorMessage ="الاسم الاول مطلوب *"), MinLength(3,ErrorMessage = "اقل حدد من الحروف فى الاسم الاول 3*"), MaxLength(20,ErrorMessage ="اكبر عدد من الحروف فى الاسم الاول 20 حرف *")]
        public string FirstName { get; set; }

        [Required(ErrorMessage ="الاسم الاخير مطلوب *"), MinLength(3,ErrorMessage =" اقل عدد من الحروف الاخير 3 حروف"), MaxLength(20,ErrorMessage = "اكبر عدد من الحروف فى الاسم الاخير 20 حرف *")]
        public string LastName { get; set; }

        [Required(ErrorMessage ="اسم الدوله مطلوب *")]
        public string Country { get; set; }

        [Required(ErrorMessage ="اسم المدينه مطلوب *")]
        public string City { get; set; }

        [Required(ErrorMessage ="بيانات الشارع مطلوبه *")]
        public string Street { get; set; }
    }
}
