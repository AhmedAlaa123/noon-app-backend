using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using noone.DTO.categoryDTO;
using noone.Reposatories.CateegoryReposatory;

namespace noone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryReposatory category;
        public CategoryController(ICategoryReposatory _category)
        {
            this.category = _category;
        }
        [HttpGet]
        public IActionResult GetCategories()
        {
            return Ok(category.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult GetCategoryById(Guid id)
        {
            if (ModelState.IsValid)
            {
                if (category.GetById(id) != null)
                    return Ok(category.GetById(id));
            }
            return BadRequest();

        }
        [HttpPost]
        public IActionResult AddCategory(AddcategoryDTO categoryDto)
        {
            if (ModelState.IsValid)
            {
                category.Insert(categoryDto);
                return StatusCode(201,categoryDto.Name+" created");
            }
            else
                return BadRequest(ModelState);
        }
        [HttpPut("{id}")]
        public IActionResult EditCategory(Guid id,AddcategoryDTO categoryDto)
        {
            if (ModelState.IsValid)
            {
                category.Update(id,categoryDto);
                return StatusCode(204,"Updated Successfully");
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(Guid id)
        {
            if (category.Delete(id))
                return Ok(" Deleted successully");
            return BadRequest("Invalid Id");
        }
    }
}
