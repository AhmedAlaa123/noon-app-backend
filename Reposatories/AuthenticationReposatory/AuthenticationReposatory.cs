using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using noone.ApplicationDTO.ApplicationUserDTO;
using noone.Helpers;
using noone.Models;

namespace noone.Reposatories.AuthenticationReposatory
{

    /// <summary>
    /// / this class used as repostory to manger user actions like register,login and roles to user
    /// </summary>
    public class AuthenticationReposatory :IAuthenticationReposatory
    {
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly RoleManager<IdentityRole> _roleManger;
        private readonly JWT _jwt;
        public AuthenticationReposatory(UserManager<ApplicationUser> userManger,RoleManager<IdentityRole> roleManager,IOptions<JWT> jwt)
        {
            this._userManger = userManger;
            this._roleManger = roleManager;
            this._jwt = jwt.Value;
        }

        public async Task<AuthenticationModel> RegisetrAsync(ApplicationUserRegisterDTO userRegisterDTO)
        {
            // check if any user has userRegister Email
            if (await _userManger.FindByEmailAsync(userRegisterDTO.Email) is not null)
            {
                new AuthenticationModel { Message = "البريد الالكترونى مستخدم " };
            }

            // check if any user has userRegister UserName;
            if (await _userManger.FindByNameAsync(userRegisterDTO.UserName) is not null)
            {
                new AuthenticationModel { Message = "اسم الستخدم مستخدم " };

            }

            // create application user
            var newUser = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = userRegisterDTO.UserName,
                Email = userRegisterDTO.Email,
                Password = userRegisterDTO.Password,
                PhoneNumber = userRegisterDTO.PhoneNumber,
                FirstName = userRegisterDTO.FirstName,
                LastName = userRegisterDTO.LastName,
                Country = userRegisterDTO.Country,
                City = userRegisterDTO.City,
                Street = userRegisterDTO.Street
            };

            //create user in the database
            var result = await this._userManger.CreateAsync(newUser, userRegisterDTO.Password);

            //check if user is not created
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error}\n";
                }
                return new AuthenticationModel { Message = errors };
            }



            return new AuthenticationModel();
        }
    }
}
