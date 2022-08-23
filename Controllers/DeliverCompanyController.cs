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
        public async Task<IActionResult> AddNew([FromHeader]string token,DeliverCompanyCreateDTO deliverCompany)
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

        [HttpDelete("{companyId}")]
        public async Task<IActionResult> DeleteDeliverCompany([FromRoute] Guid companyId)
        {
            //if(string.IsNullOrWhiteSpace(Token)||string.IsNullOrEmpty(companyId.ToString()))
            //{
            //    return BadRequest("الرقم التعريفى للشركه او بيانات المستخدم خطأ");
            //}

            //// check if user is authorized
            //if (!string.IsNullOrEmpty(await CheckUseIsAdminOrEmployee(Token)))
            //    return Unauthorized(await CheckUseIsAdminOrEmployee(Token));

            // delete company
            bool isDeleted=await this._deliverCompanyReposatory.Delete(Id:companyId);

            if (!isDeleted)
                return BadRequest("حدث خطأ لم يتم حذف الشركه");
            return Ok();
        }

        [HttpGet("allDeliverCompanies")]
        public async Task<IActionResult> GetAllDeliverCompanies()
        {
            return Ok(await this._deliverCompanyReposatory.GetAll());
        }

        [HttpGet("allDeliverCompanies/{companyId}")]
        public async Task<IActionResult> GetDeliverCompanyById(Guid companyId)
        {
            DeliverCompany company=await this._deliverCompanyReposatory.GetById(companyId);
            if(company is null)
            {
                return NotFound("الشركه ليست موجوده");
            }

            DeliverCompanyInfoDTO deliverCompany = new DeliverCompanyInfoDTO
            {
                Id = company.Id,
                Name = company.Name,
                Address = company.Address,
                ContactNumber = company.ContactNumber
            };

            return Ok(deliverCompany);
        }

        private async Task<string> CheckUseIsAdminOrEmployee(string Token)
        {
            var jwtSecurity = TokenConverter.ConvertToken(Token);
            if (jwtSecurity is  null)
                return " غير مسموح لك الاضافه32";

            var user = await this._useManger.FindByNameAsync(jwtSecurity.Subject);
            if (user is  null || !await this._useManger.IsInRoleAsync(user, Roles.ADMIN_ROLE) && !await this._useManger.IsInRoleAsync(user, Roles.EMPLOYEE_ROLE))
                return "غير مسموح لك الاضافه";  
            return string.Empty;
        }

        //Edit Company 

      
        [HttpPut("edit/{token}/{id}")]
        public async Task<IActionResult> UpdateDeliverCompany( [FromRoute]string token, [FromBody] DeliverCompanyCreateDTO deliverCompany , [FromRoute] Guid id)   
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            // check if user is Admin Or Employee
            if (!string.IsNullOrEmpty(await CheckEditIsAdminOrEmployee(token)))
                return Unauthorized(await CheckEditIsAdminOrEmployee(token));

            DeliverCompany UpdatedDeliverCompany = new DeliverCompany { 
                Name = deliverCompany.Name,
                ContactNumber=deliverCompany.ContactNumber,
                Address=deliverCompany.Address
            };
            bool isUpdated = await this._deliverCompanyReposatory.Update(id, UpdatedDeliverCompany);

           

            if (!isUpdated)
                return BadRequest("لم يتم التعديل اعد المحاوله");

            return Ok(deliverCompany);



        }

        private async Task<string> CheckEditIsAdminOrEmployee(string Token)
        {
            var jwtSecurity = TokenConverter.ConvertToken(Token);
            if (jwtSecurity == null)
                return " غير مسموح لك  بالتعديل ";

            var user = await this._useManger.FindByNameAsync(jwtSecurity.Subject);
            if (user == null || !await this._useManger.IsInRoleAsync(user, Roles.ADMIN_ROLE) && !await this._useManger.IsInRoleAsync(user, Roles.EMPLOYEE_ROLE))
                return $"{!await this._useManger.IsInRoleAsync(user, Roles.EMPLOYEE_ROLE)}  غير مسموح لك  بالتعديل ";
            return string.Empty;
        }

    }
}
