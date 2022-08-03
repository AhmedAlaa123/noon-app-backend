using Microsoft.AspNetCore.Identity;
using noone.ApplicationDTO.ApplicationUserDTO;
using noone.Models;
using noone.Helpers;
using Microsoft.Extensions.Options;

namespace noone.Reposatories.AuthenticationReposatory
{
    

    public class AuthenticationReposatory :IAuthenticationReposatory
    {
        private readonly UserManager<ApplicationUser> _userManager ;
        private readonly JWT _jwt;
        public AuthenticationReposatory(UserManager<ApplicationUser> userManager , IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
        }

        public async Task<AuthenticationModel> GetTokenAsync(ApplicationUserSignInDTO model)
        {
            var AuthModel = new AuthenticationModel();
            //validation on username and password 

            //user found or not at database  
            var User = await _userManager.FindByNameAsync(model.UserName);

            //if user not found  or  password not matched
            // check first on existing user then check password matching 
            if (User is null || !await _userManager.CheckPasswordAsync(User, model.Password)) 
            {
                AuthModel.Message = "Email or Password is in correct ";
                return AuthModel;
            }
           // var jwtsecurityToken = await
            AuthModel.IsAuthenticated = true;


            return AuthModel;
        }


    }
}
