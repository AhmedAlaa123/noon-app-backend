using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using noone.ApplicationDTO.ApplicationUserDTO;
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

            return Ok(result);
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
            return Ok(authentcationModel);
        }

        [HttpPost("addRole")]
        public async Task<IActionResult> AddRole(ApplicationUserAddRoleDTO userAddRoleDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string message = await this._authenticationReposatory.AddUserToRole(userAddRoleDTO);
            
            if (!string.IsNullOrEmpty(message))
                return BadRequest(message);

            return Ok(userAddRoleDTO);
        }
        [HttpDelete("removeUser/{deletedUserId}")]
        public async Task<IActionResult> RemoveUser([FromBody]string JWTToken,[FromRoute] string deletedUserId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string message = await this._authenticationReposatory.RemoveUser(JWTToken, deletedUserId);

            if (!string.IsNullOrEmpty(message))
                return BadRequest(message);

            return Ok("تم حذف المستخدم بنجاح");
        }
        [HttpDelete("removeRole")]
        public async Task<IActionResult> RemoveRole(ApplicationUserAddRoleDTO userAddRoleDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string message = await this._authenticationReposatory.RemoveRoleFromUser(userAddRoleDTO);

            if (!string.IsNullOrEmpty(message))
                return BadRequest(message);

            return Ok("تم حذف الوظيفه من على المستخدم");
        }

        [HttpGet("users/{userToken}")]
        public async Task<ActionResult> GetAllUsers(string userToken)
        {
            if (userToken == null)
                return BadRequest("userToken Must not be null");
            var users = await this._authenticationReposatory.GetAllUsers(userToken);
            if (users is null)
                return BadRequest("غير مسموح لك");
            return Ok(users);
        }
    }
}
