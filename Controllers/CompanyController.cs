using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using noone.ApplicationDTO.CompanyDTO;
using noone.Contstants;
using noone.Helpers;
using noone.Models;
using noone.Reposatories;

namespace noone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _useManger;
        private readonly IReposatory<Company> _CompanyReposatory;
        IWebHostEnvironment env;

        public CompanyController(UserManager<ApplicationUser> useManger, IReposatory<Company> CompanyReposatory, IWebHostEnvironment env)
        {
            this._useManger = useManger;
            this._CompanyReposatory = CompanyReposatory;
            this.env = env;
        }


        [HttpPost("AddNew")]
        public async Task<IActionResult> AddNew(string token, [FromForm] CompanyCreateDTO Company)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            // check if user is Admin Or Employee
            if (!string.IsNullOrEmpty(await CheckUseIsAdminOrEmployee(token)))
                return Unauthorized(await CheckUseIsAdminOrEmployee(token));
            //upload image
            string uploadimg = Path.Combine(env.WebRootPath, "images/CompaniesImages");
            string uniqe = Guid.NewGuid().ToString() + "_" + Company.BrandImage.FileName; ;
            string pathfile = Path.Combine(uploadimg, uniqe);
            using (var filestream = new FileStream(pathfile, FileMode.Create))
            {
                Company.BrandImage.CopyTo(filestream);
                filestream.Close();
            }
            Company company = new Company
            {
                Name = Company.Name,
                ContactNumber = Company.ContactNumber,
                BrandImage = pathfile
            };

            bool isInserted = await this._CompanyReposatory.Insert(company);

            if (!isInserted)
                return BadRequest("لم يتم الاضافه اعد المحاوله");

            return Ok(Company);



        }

        [HttpDelete("{companyId}")]
        public async Task<IActionResult> DeleteCompany([FromRoute] Guid companyId, [FromBody] string Token)
        {
            if (string.IsNullOrWhiteSpace(Token) || string.IsNullOrEmpty(companyId.ToString()))
            {
                return BadRequest("الرقم التعريفى للشركه او بيانات المستخدم خطأ");
            }

            // check if user is authorized
            if (!string.IsNullOrEmpty(await CheckUseIsAdminOrEmployee(Token)))
                return Unauthorized(await CheckUseIsAdminOrEmployee(Token));

            // delete company
            bool isDeleted = await this._CompanyReposatory.Delete(Id: companyId);

            if (!isDeleted)
                return BadRequest("حدث خطأ لم يتم حذف الشركه");
            return Ok("تم حذف الشركه");
        }

        [HttpGet("allCompanies")]
        public async Task<IActionResult> GetAllCompanies()
        {
            return Ok(await this._CompanyReposatory.GetAll());
        }

        [HttpGet("allCompanies/{companyId}")]
        public async Task<IActionResult> GetCompanyById(Guid companyId)
        {
            Company company = await this._CompanyReposatory.GetById(companyId);
            if (company is null)
            {
                return NotFound("الشركه ليست موجوده");
            }

            CompanyInfoDTO Company = new CompanyInfoDTO
            {
                Id = company.Id,
                Name = company.Name,
                ContactNumber = company.ContactNumber,
                BrandImage = company.BrandImage
            };

            return Ok(Company);
        }

        private async Task<string> CheckUseIsAdminOrEmployee(string Token)
        {
            var jwtSecurity = TokenConverter.ConvertToken(Token);
            if (jwtSecurity is null)
                return " غير مسموح لك الاضافه32";

            var user = await this._useManger.FindByNameAsync(jwtSecurity.Subject);
            if (user is null || !await this._useManger.IsInRoleAsync(user, Roles.ADMIN_ROLE) && !await this._useManger.IsInRoleAsync(user, Roles.EMPLOYEE_ROLE))
                return "غير مسموح لك الاضافه";
            return string.Empty;
        }

        //Edit Company 


        [HttpPut("edit/{token}/{id}")]
        public async Task<IActionResult> UpdateCompany([FromRoute] string token, [FromForm] CompanyUpdatedto company, [FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            // check if user is Admin Or Employee
            if (!string.IsNullOrEmpty(await CheckEditIsAdminOrEmployee(token)))
                return Unauthorized(await CheckEditIsAdminOrEmployee(token));
           
            Company UpdatedCompany = new Company();
            if (company.BrandImage != null)
            {

                //upload image
                string uploadimg = Path.Combine(env.WebRootPath, "images/CompaniesImages");
                string uniqe = Guid.NewGuid().ToString() + "_" + company.BrandImage.FileName;
                string pathfile = Path.Combine(uploadimg, uniqe);
                using (var filestream = new FileStream(pathfile, FileMode.Create))
                {
                    company.BrandImage.CopyTo(filestream);
                    filestream.Close();
                }
                UpdatedCompany.BrandImage = pathfile;
            }
            UpdatedCompany.Name = company.Name;
            UpdatedCompany.ContactNumber = company.ContactNumber;


              
           

            bool isUpdated = await this._CompanyReposatory.Update(id, UpdatedCompany);



            if (!isUpdated)
                return BadRequest("لم يتم التعديل اعد المحاوله");

            return Ok(company);



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
