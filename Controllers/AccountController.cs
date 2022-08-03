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

        private readonly IAuthenticationReposatory _authRepo;

        public AccountController(IAuthenticationReposatory AuthRepo)
        {
            _authRepo = AuthRepo;
        }

        [HttpPost("SignIn")] //GetTokenAsync
        public async Task<IActionResult> GetTokenAsync([FromBody] ApplicationUserSignInDTO userSignInDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authRepo.GetTokenAsync(userSignInDTO);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
