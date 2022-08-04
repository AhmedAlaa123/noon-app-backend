using Microsoft.AspNetCore.Identity;
using noone.CustomAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace noone.Models
{
    [Table("User")]
    public class ApplicationUser:IdentityUser
    {

        [Required, UniqueUserName]
        public override string UserName { get; set; }
        [Required, MinLength(8)]
        public string Password { get; set; }

        [Required,StringLength(11)]
        public override string PhoneNumber { get; set; }

        [Required,MinLength(3),MaxLength(20)]
        public string FirstName { get; set; }
        

        [Required,MinLength(3),MaxLength(20)]
        public string LastName { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City{ get; set; }

        [Required]
        public string Street{ get; set; }

        // Products That had added by this user
        public ICollection<Product> Products { get; set; }
        // orders that had been ordered by this user
        public ICollection<Order> Orders { get; set; }


    }
}
