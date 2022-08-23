using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using noone.ApplicationDTO.OrderDTO;
using noone.Contstants;
using noone.Helpers;
using noone.Models;
using noone.Reposatories;
using noone.Reposatories.OrderReposatory;

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
        //[Authorize]
        [HttpPost("AddNew")]
        public async Task<IActionResult> AddNew([FromHeader]string token, OrderCreateDTO order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            // check if user is Admin Or Employee
            //if (!string.IsNullOrEmpty(await CheckUseIsAdminOrEmployee(token)))
            //    return Unauthorized(await CheckUseIsAdminOrEmployee(token));
            var jwtSecurity = TokenConverter.ConvertToken(token);
            if (jwtSecurity is null)
                return BadRequest(" غير مسموح لك الاضافه");
            var user = await this._useManger.FindByNameAsync(jwtSecurity.Subject);
            if (user is null)
                return BadRequest("المستخدم غير مجود");
            DateTime orderDate = DateTime.Now;
            Order ord = new Order
            {
                DeliverDate = orderDate.AddDays(3),
                OrderDate = orderDate,
                UserId=user.Id

            };

            bool isInserted = await this._order.Insert(ord);
            var orderRepo = (OrderReposatory)this._order;
            bool productIsAdded = await orderRepo.AddProductsToOrder(order.Products);
            
            if (!isInserted||!productIsAdded)
                return BadRequest("لم يتم الاضافه اعد المحاوله");

            // create Bill

            return Ok("تم ان  شاء الله الاوردر");

        }
        private async Task<string> CheckUseIsAdminOrEmployee(string Token)
        {
            var jwtSecurity = TokenConverter.ConvertToken(Token);
            if (jwtSecurity is null)
                return " غير مسموح لك الاضافه";

            var user = await this._useManger.FindByNameAsync(jwtSecurity.Subject);
            if (user is null || !await this._useManger.IsInRoleAsync(user, Roles.ADMIN_ROLE) && !await this._useManger.IsInRoleAsync(user, Roles.EMPLOYEE_ROLE))
                return "غير مسموح لك الاضافه";
            return string.Empty;
        }

        //Edit Company 
        [Authorize(Roles =$"{Roles.USER_ROLE},{Roles.EMPLOYEE_ROLE}")]

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderUpdateDTO ord, [FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            // check if user is Admin Or Employee
            //string message = await CheckEditIsAdminOrEmployee(token);
            //if (!string.IsNullOrEmpty(message))
            //    return Unauthorized(message);

            Order Updatedorder = new Order
            {
               DeliverDate= ord.DeliverDate,
               OrderDate= ord.OrderDate

            };
            bool isUpdated = await this._order.Update(id, Updatedorder);



            if (!isUpdated)
                return BadRequest();

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


       // delete Order
       [HttpDelete("{orderId}")]
        public async Task<IActionResult> CancelOrder([FromRoute] Guid orderId)
        {
            if (orderId == null||! await this._order.Delete(orderId))
                return BadRequest("رقم الطلب غير صالح");
            return Ok("تم الغاء الطلب");
        }

        [HttpGet("{token}")]
        public async Task<IActionResult> GetAllOrders(string token)
        {
            string message = await CheckEditIsAdminOrEmployee(token);
            if (!string.IsNullOrEmpty(message))
                return Unauthorized(message);

            return Ok(await this._order.GetAll());
        }


    }
}
