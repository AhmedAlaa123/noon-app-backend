using Microsoft.AspNetCore.Mvc;
using noone.Models;
using noone.Reposatories;
using noone.ApplicationDTO.BillDTO;
using Microsoft.AspNetCore.Authorization;
using noone.Contstants;

namespace noone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IReposatory<Bill> _billReposatroy;
        public BillController(IReposatory<Bill> billReposatroy)
        {
            this._billReposatroy = billReposatroy;
        }
        [HttpGet("allBills")]
        [Authorize(Roles=$"{Roles.ADMIN_ROLE},{Roles.USER_ROLE}")]
        public async Task<IActionResult> GetAllBills()
        {
            return Ok(await this._billReposatroy.GetAll());
        }

        [HttpGet("bills/{Id}")]
        public async Task<IActionResult> GetBillById(Guid Id)
        {
            var bill = await this._billReposatroy.GetById(Id);
            if (bill is null)
                return BadRequest("الفاتوره غيره موجوده");
            BillInfoDTO billInfo = new BillInfoDTO
            {
                Id = bill.Id,
                Price = bill.Price,
                Order_Id = bill.Order_Id,
            };
            return Ok(billInfo);
        }
        [HttpPost("addNewBill")]
        public async Task<IActionResult> AddNewBill([FromBody]BillCreateDTO bill)
        {
            if(!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Bill newBill = new Bill
            {
                Order_Id=bill.Order_Id,
                Price=bill.Price
            };
            bool isInserted = await this._billReposatroy.Insert(newBill);
            if (!isInserted)
                return BadRequest("لم يتم اضافه الفاتوره حاول مره اخرى");
          
            // return 201 created
            return StatusCode(StatusCodes.Status201Created,newBill);
        }

        [HttpPut("bill/{Id}")]
        public async Task<IActionResult> UpdateBill([FromBody]BillCreateDTO billCreateDTO,Guid Id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Bill bill = new Bill
            {
                Price = billCreateDTO.Price,
                Order_Id = billCreateDTO.Order_Id
            };

            bool isUpdated = await this._billReposatroy.Update(Id,bill);
            if (!isUpdated)
                return BadRequest("لم يتم تحديث البيانات اعد المحاوله");
            return Ok("تم تحديث البيانات");

        }

    }
}
