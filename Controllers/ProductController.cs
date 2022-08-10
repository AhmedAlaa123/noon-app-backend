using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using noone.ApplicationDTO.ProductDTO;
using noone.Models;
using noone.Reposatories;
using noone.Reposatories.ProductReposatory;

namespace noone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        readonly NoonEntities context;
        readonly IProductReposatory product;
        public ProductController(IProductReposatory _product, NoonEntities _context)
        {
            this.product = _product;
            this.context= _context;

        }
        [HttpGet]
        public  IActionResult GetAllProd()
        {
            
         
            return Ok(product.GetAll());
        }
        [HttpPost]
        public  IActionResult AddProduct(PoductAddDto productDto)
        {
            Product newProduct=new Product();
            if (ModelState.IsValid)
            {
                product.Insert(productDto);
                    return StatusCode(201, "تمت اضافة المنتج");
               
                
            }
            return BadRequest(ModelState);

        }
        [HttpGet("{Id}")]
        public IActionResult getById(Guid Id)
        {
            if (product.GetById(Id) != null)
                return Ok(product.GetById(Id));
            return BadRequest(ModelState);
        }
        [HttpDelete("{Id}")]
        public IActionResult deleteProduct(Guid Id)
        {
            if (product.Delete(Id))
                return Ok("تم حذف العنصر");
            return BadRequest("لم يتم الحذف");
        }
        [HttpPut("{Id}")]
        public IActionResult Edit(Guid Id, PoductAddDto item)
        {
            if (ModelState.IsValid) {
                if (product.Update(Id, item))
                    return Ok("تم تعديل المنتج");
                return BadRequest("لم يتم تعديل المنتج");
                      
            }
            return BadRequest(ModelState);
        }

    }
}
