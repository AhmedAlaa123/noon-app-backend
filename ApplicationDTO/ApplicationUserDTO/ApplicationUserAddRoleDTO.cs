using System.ComponentModel.DataAnnotations;

namespace noone.ApplicationDTO.ApplicationUserDTO
{
    public class ApplicationUserAddRoleDTO
    {
       [Required(ErrorMessage ="رقم المستخدم مطلوب *")]
        public string UserId { get; set; }
        [Required(ErrorMessage ="الوظيفه مطلوبه *")]
        public string Role { get; set; }
    }
}
