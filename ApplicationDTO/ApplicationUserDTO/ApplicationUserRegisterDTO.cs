using noone.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace noone.ApplicationDTO.ApplicationUserDTO
{
    public class ApplicationUserRegisterDTO
    {

        [Required]
        public string Email { get;set; }

        [Required, UniqueUserName]
        public string UserName { get; set; }

        [Required, MinLength(8)]
        public string Password { get; set; }

        [Required, StringLength(11), RegularExpression("^0[1-9]{10}$")]
        public  string PhoneNumber { get; set; }

        [Required, MinLength(3), MaxLength(20)]
        public string FirstName { get; set; }

        [Required, MinLength(3), MaxLength(20)]
        public string LastName { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Street { get; set; }
    }
}
