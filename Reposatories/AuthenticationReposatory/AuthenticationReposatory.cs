using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using noone.ApplicationDTO.ApplicationUserDTO;
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


        //sign In 
        public async Task<AuthenticationModel> GetTokenAsync(ApplicationUserSignInDTO userSignInDTO)
        {
            var AuthModel = new AuthenticationModel();
            //validation on username and password 

            //user found or not at database  
            var User = await _userManger.FindByNameAsync(userSignInDTO.UserName);

            //if user not found  or  password not matched
            // check first on existing user then check password matching 
            if (User is null || !await _userManger.CheckPasswordAsync(User, userSignInDTO.Password))
            {
                AuthModel.Message = "الايميل او كلمة المرور غير متاحة ";
                return AuthModel;
            }
            var jwtsecurityToken = await CreateJwtToken(User);
            var rolesList = await _userManger.GetRolesAsync(User);
            AuthModel.IsAuthenticated = true;
            AuthModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtsecurityToken);
            AuthModel.Email = User.Email;
            AuthModel.Username = User.UserName;
            AuthModel.ExpiresOn = jwtsecurityToken.ValidTo;
            AuthModel.Roles = rolesList.ToList();



            return AuthModel;
        }

        // function to create JWT

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManger.GetClaimsAsync(user);//user claims
            var roles = await _userManger.GetRolesAsync(user);      //user roles
            var roleClaims = new List<Claim>();                     //claims of roles

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id) 
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

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
