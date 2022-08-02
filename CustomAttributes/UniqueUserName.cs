using noone.Models;
using System.ComponentModel.DataAnnotations;

namespace noone.CustomAttributes
{

    /// <summary>
    ///  this class used to validate user name to be unique
    /// </summary>
    public class UniqueUserName : ValidationAttribute
    {
        NoonEntities _context = new NoonEntities();

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            // check if user name is not inserted

            if (value == null) return new ValidationResult("*اسم المستدم مطلوب!");

            string userName = value.ToString().Trim().ToLower();

            // get the user that has this user name

            ApplicationUser user = _context.Users.FirstOrDefault(user => user.UserName.ToLower().Equals(userName));

            // check if any user has this username
            // if true return error message else return success
            if (user != null) return new ValidationResult("*اسم المستخدم هذا مستخدم ");

            return ValidationResult.Success;


        }


    }
}
