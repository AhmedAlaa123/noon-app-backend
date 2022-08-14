using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using noone.ApplicationDTO.ApplicationUserDTO;
using noone.Contstants;
using noone.Reposatories.AuthenticationReposatory;


namespace noone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IAuthenticationReposatory _authenticationReposatory;
       

        public AccountController(IAuthenticationReposatory AuthRepo)
        {
            _authenticationReposatory = AuthRepo;
        }

        [HttpPost("SignIn")] //GetTokenAsync
        public async Task<IActionResult> GetTokenAsync([FromBody] ApplicationUserSignInDTO userSignInDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authenticationReposatory.GetTokenAsync(userSignInDTO);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(new {Token=result.Token,Expiration=result.ExpiresOn});
        }
       
        [HttpPost("SignOut")]
        public async Task<IActionResult> SignOut([FromBody] string UserName)
        {
            var test = await this._authenticationReposatory.SignOutAsync(UserName);
            return Ok(test);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]ApplicationUserRegisterDTO userRegisterDTO)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var authentcationModel = await this._authenticationReposatory.RegisetrAsync(userRegisterDTO);

            if(!authentcationModel.IsAuthenticated)
            {
                return BadRequest(authentcationModel.Message);
            }

            return Ok(new { Token = authentcationModel.Token, Expiration = authentcationModel.ExpiresOn });
       
        }

        [HttpPost("addRole")]
        [Authorize(Roles = $"{Roles.ADMIN_ROLE}")]
        public async Task<IActionResult> AddRole(ApplicationUserAddRoleDTO userAddRoleDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string message = await this._authenticationReposatory.AddUserToRole(userAddRoleDTO);
            
            if (!string.IsNullOrEmpty(message))
                return BadRequest(message);

            return Ok(userAddRoleDTO);
        }
        [Authorize(Roles = $"{Roles.ADMIN_ROLE}")]
        [HttpDelete("{deletedUserId}")]
        public async Task<IActionResult> RemoveUser([FromBody]string JWTToken,[FromRoute] string deletedUserId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string message = await this._authenticationReposatory.RemoveUser(JWTToken, deletedUserId);

            if (!string.IsNullOrEmpty(message))
                return BadRequest(message);

            return Ok("تم حذف المستخدم بنجاح");
        }
        [Authorize(Roles = $"{Roles.ADMIN_ROLE}")]
        [HttpDelete("removeRole")]
        public async Task<IActionResult> RemoveRole(ApplicationUserAddRoleDTO userAddRoleDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string message = await this._authenticationReposatory.RemoveRoleFromUser(userAddRoleDTO);

            if (!string.IsNullOrEmpty(message))
                return BadRequest(message);

            return Ok();
        }

        [Authorize(Roles = $"{Roles.ADMIN_ROLE}")]
        [HttpGet("users")]
        public async Task<ActionResult> GetAllUsers()
        {
            var users = await this._authenticationReposatory.GetAllUsers();
            return Ok(users);
        }
    }
}
