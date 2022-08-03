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

        public AccountController(IAuthenticationReposatory authenticationReposatory)
        {
            _authenticationReposatory = authenticationReposatory;
        }

        [HttpPost("/Register")]
        public async Task<IActionResult> Register(ApplicationUserRegisterDTO userRegisterDTO)
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
    }
}
