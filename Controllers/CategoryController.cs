using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using noone.ApplicationDTO.categoryDTO;
using noone.Reposatories;
using noone.Models;
using System.Collections.Generic;

namespace noone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IReposatory<Category> categoryReposatry;
        public CategoryController(IReposatory<Category> _categoryReposatry)
        {
            this.categoryReposatry = _categoryReposatry;
        }
        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            List<CategoryInfoDTO> categories = new List<CategoryInfoDTO>();
            foreach(var cate in await categoryReposatry.GetAll())
            {
                categories.Add(new CategoryInfoDTO { Id=cate.Id,Name=cate.Name});
            }
            return Ok(categories);
        }
        [HttpGet("categories/{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
           
                Category category = await categoryReposatry.GetById(id);
                if ( category != null)
                    return Ok(category);
            return BadRequest($"غير موجود {id}");

        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryCreateDTO categoryDto)
        {
            if (ModelState.IsValid)
            {
                Category newCategory = new Category
                {
                    Name=categoryDto.Name,
                    
                };
                bool isAdded=await categoryReposatry.Insert(newCategory);
                if (isAdded)
                    return StatusCode(201, categoryDto.Name + " تم اضافه");
                else
                    return BadRequest("لم الاضافه بنجاح");
            }
            else
                return BadRequest(ModelState);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCategory(Guid id,AddcategoryDTO categoryDto)
        {
            
            if (ModelState.IsValid)
            {
                Category CategoryWithUpdate = new Category { Name = categoryDto.Name };
               bool isUpdated=await categoryReposatry.Update(id, CategoryWithUpdate);

                if(isUpdated)
                return StatusCode(204,"تم تعديل البيانات");
                else
                    BadRequest("حدث خطا اعد المحاوله");
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            if (await categoryReposatry.Delete(id))
                return Ok("تم الحذف بنجاح");
            return BadRequest($"غير متاح {id.ToString()}");
        }
    }
}
