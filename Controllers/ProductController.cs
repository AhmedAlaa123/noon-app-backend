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
        [HttpGet("allpro")]
        public  IActionResult GetAllProd()
        {
            var products = product.GetAll();
            foreach(var pro in products)
            {
             
                pro.ProductImage = $"https://{HttpContext.Request.Host.Value}/images/ProductsImages/{pro.ProductImage}";
            }
            return Ok(products);
        }
        [HttpPost("addNewPro")]
        public  IActionResult AddProduct([FromForm]PoductAddDto productDto,[FromHeader]string token)
        {
            Product newProduct=new Product();
            if (ModelState.IsValid)
            {
                product.Insert(productDto,token);
                    return StatusCode(201);  
            }
            return BadRequest(ModelState);

        }
        [HttpGet("{Id}")]
        public IActionResult getById(Guid Id)
        {
            if (product.GetById(Id) != null) {
                var pro = product.GetById(Id);
                pro.ProductImage = $"https://{HttpContext.Request.Host.Value}/images/ProductsImages/{pro.ProductImage}";
                return Ok(pro);
            }
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
        public IActionResult Edit([FromRoute]Guid Id, PoductAddDto item)
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
