using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using noone.ApplicationDTO.OrderDTO;
using noone.Contstants;
using noone.Helpers;
using noone.Models;
using noone.Reposatories;

namespace noone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly UserManager<ApplicationUser> _useManger;
        private readonly IReposatory<Order> _order;

        public OrderController(UserManager<ApplicationUser> useManger, IReposatory<Order> orderReposatory)
        {
            this._useManger = useManger;
            this._order = orderReposatory;
        }

        [HttpPost("AddNew")]
        public async Task<IActionResult> AddNew(string token, OrderCreateDTO order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            // check if user is Admin Or Employee
            if (!string.IsNullOrEmpty(await CheckUseIsAdminOrEmployee(token)))
                return Unauthorized(await CheckUseIsAdminOrEmployee(token));
            Order ord = new Order
            {
                DeliverDate = order.DeliverDate,
                OrderDate = order.OrderDate

            };

            bool isInserted = await this._order.Insert(ord);

            if (!isInserted)
                return BadRequest("لم يتم الاضافه اعد المحاوله");

            return Ok(ord);



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
        public async Task<IActionResult> UpdateDeliverCompany([FromRoute] string token, [FromBody] OrderCreateDTO ord, [FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            // check if user is Admin Or Employee
            if (!string.IsNullOrEmpty(await CheckEditIsAdminOrEmployee(token)))
                return Unauthorized(await CheckEditIsAdminOrEmployee(token));

            Order Updatedorder = new Order
            {
               DeliverDate= ord.DeliverDate,
               OrderDate= ord.OrderDate

            };
            bool isUpdated = await this._order.Update(id, Updatedorder);



            if (!isUpdated)
                return BadRequest("لم يتم التعديل اعد المحاوله");

            return Ok(ord);

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
