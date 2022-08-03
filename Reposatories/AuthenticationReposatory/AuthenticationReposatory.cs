using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using noone.ApplicationDTO.ApplicationUserDTO;
using noone.Contstants;
using noone.Helpers;
using noone.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
                new AuthenticationModel { Message = getErrorsAsString(result)};

            // assign the new user to Role as User
            result= await this._userManger.AddToRoleAsync(newUser, Roles.USER_ROLE);
            if (!result.Succeeded)
                new AuthenticationModel { Message = getErrorsAsString(result) };

            var jwtSecurityToken = await this.CreateJwtToken(newUser);
            return new AuthenticationModel
            {
                Email = newUser.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> {Roles.USER_ROLE },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = newUser.UserName
            };

        }



        // this method used to get result errors as string
        private string getErrorsAsString(IdentityResult result)
        {
            var errors = string.Empty;
            foreach (var error in result.Errors)
            {
                errors += $"{error}\n";
            }
            return errors;
        }


        // this method to generate JWT Token

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {

            // get user climes
            var userClimes = await this._userManger.GetClaimsAsync(user);

            // get user Roles
            var roles=await this._userManger.GetRolesAsync(user);

            var roleClaims = new List<Claim>();
            foreach(var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));

            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email)
            }.Union(userClimes).Union(roleClaims);

            var symmetricSecurityKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            // create token
            var jwtSecurityToken = new JwtSecurityToken(
               issuer: _jwt.Issuer,
               audience: _jwt.Audience,
               claims: claims,
               expires: DateTime.Now.AddDays(_jwt.DurationInDays),
               signingCredentials: signingCredentials);

            return jwtSecurityToken;



        }


    }
    
}
