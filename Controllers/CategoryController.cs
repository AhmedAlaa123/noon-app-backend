using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using noone.ApplicationDTO.categoryDTO;
using noone.Reposatories;
using noone.Models;
using System.Collections.Generic;
using noone.ApplicationDTO.SubCategoryDto;
using noone.ApplicationDTO.ProductDTO;

namespace noone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        //private readonly string subCateroriesImagesUrl;
        private readonly IReposatory<Category> categoryReposatry;
        public CategoryController(IReposatory<Category> _categoryReposatry)
        {
            this.categoryReposatry = _categoryReposatry;
            
        }
        [HttpGet("All")]
        public async Task<IActionResult> GetCategories()
        {
            List<CategoryInfoDTO> categories = new List<CategoryInfoDTO>();
            string basUrl = $"https://{HttpContext.Request.Host.Value}/images/";
            foreach (var cate in await categoryReposatry.GetAll())
            {
                CategoryInfoDTO categoryInfo = new CategoryInfoDTO()
                {
                    Id = cate.Id,
                    Name = cate.Name
                };
                categoryInfo.SubCategories = new List<SubCategoryInfoDTO>();
                foreach(var subcate in cate.SubCategories)
                {
                    categoryInfo.SubCategories.Add(new SubCategoryInfoDTO
                    {
                        SubCategoryId = subcate.Id,
                        SubCategoryName = subcate.Name,
                        SubCategoryImage = $"{basUrl}subCategoryImages/{ subcate.Image}"
                    }) ;  

                }
                categoryInfo.Products = new List<ProductInfoDto>();
                foreach (var product in cate.Products)
                {
                    ProductInfoDto productInfo = new ProductInfoDto
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        Description = product.Description,
                        ProductImage= $"{basUrl}ProductsImages/{product.Image}"
                    };
                    //foreach (var img in product.ProductImages)
                    //{
                    //    productInfo.ProductImages.Add($"{basUrl}ProductsImages/{img.Image}");
                    //};
                    categoryInfo.Products.Add(productInfo);
                }
                categories.Add(categoryInfo);
            }
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            Category category = await categoryReposatry.GetById(id);
                if ( category is  null)
                    return BadRequest($"غير موجود {id}");

            CategoryInfoDTO categoryInfo = new CategoryInfoDTO { Id = category.Id, Name = category.Name };
            List<SubCategoryInfoDTO> subCategoryInfos = new List<SubCategoryInfoDTO>();
            string basUrl = $"https://{HttpContext.Request.Host.Value}/images/";
            foreach(var subCategory in category.SubCategories)
            {
                subCategoryInfos.Add(
                new SubCategoryInfoDTO
                {
                    SubCategoryId=subCategory.Id,
                    SubCategoryName=subCategory.Name,
                    SubCategoryImage= $"{basUrl}subCategoryImages/" + subCategory.Image
                });

            }
            categoryInfo.Products = new List<ProductInfoDto>();
            foreach(var product in category.Products)
            {
                ProductInfoDto productInfo = new ProductInfoDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description,
                    ProductImage = $"{basUrl}ProductsImages/{product.Image}"
                };
                //foreach(var img in product.ProductImages)
                //{
                //    productInfo.ProductImages.Add($"{basUrl}ProductsImages/{img.Image}");
                //};
                categoryInfo.Products.Add(productInfo);
            }
            categoryInfo.SubCategories = subCategoryInfos;

               return Ok(categoryInfo);

        }
        [HttpPost("addNew")]
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
        public async Task<IActionResult> EditCategory(Guid id,CategoryCreateDTO categoryDto)
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
