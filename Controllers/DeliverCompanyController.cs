using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using noone.ApplicationDTO.DeliverCompanyDTO;
using noone.Contstants;
using noone.Helpers;
using noone.Models;
using noone.Reposatories;

namespace noone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliverCompanyController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _useManger;
        private readonly IReposatory<DeliverCompany> _deliverCompanyReposatory;

        public DeliverCompanyController(UserManager<ApplicationUser> useManger, IReposatory<DeliverCompany> deliverCompanyReposatory)
        {
            this._useManger = useManger;
            this._deliverCompanyReposatory = deliverCompanyReposatory;
        }


        [HttpPost("AddNew")]
        public async Task<IActionResult> AddNew(string token,DeliverCompanyCreateDTO deliverCompany)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            // check if user is Admin Or Employee
            if (!string.IsNullOrEmpty(await CheckUseIsAdminOrEmployee(token)))
                return Unauthorized(await CheckUseIsAdminOrEmployee(token));
            DeliverCompany company = new DeliverCompany
            {
                Name=deliverCompany.Name,
                ContactNumber=deliverCompany.ContactNumber,
                Address=deliverCompany.Address
            };

            bool isInserted = await this._deliverCompanyReposatory.Insert(company);

            if (!isInserted)
                return BadRequest("لم يتم الاضافه اعد المحاوله");

            return Ok(deliverCompany);
            
         

        }

        private async Task<string> CheckUseIsAdminOrEmployee(string Token)
        {
            var jwtSecurity = TokenConverter.ConvertToken(Token);
            if (jwtSecurity == null)
                return " غير مسموح لك الاضافه32";

            var user = await this._useManger.FindByNameAsync(jwtSecurity.Subject);
            if (user == null || !await this._useManger.IsInRoleAsync(user, Roles.ADMIN_ROLE) && !await this._useManger.IsInRoleAsync(user, Roles.EMPLOYEE_ROLE))
                return $"{!await this._useManger.IsInRoleAsync(user, Roles.EMPLOYEE_ROLE)} 323غير مسموح لك الاضافه";
            return string.Empty;
        }

    }
}
