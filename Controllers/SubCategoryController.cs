using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using noone.Models;
using noone.Reposatories;
using noone.Reposatories.SubCategoryReposatory;
using noone.ApplicationDTO.SubCategoryDto;

namespace noone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        IReposatory<SubCategory> reposatory;
        IWebHostEnvironment env;
        public SubCategoryController(IReposatory<SubCategory> subCategory, IWebHostEnvironment env)
        {
            reposatory = subCategory;
            this.env = env;
        }
        [HttpGet]
        public IActionResult GetallSupcategory()
        {
            var supcategories = reposatory.GetAll();
            return Ok(supcategories);
        }
        [HttpGet]
        [Route("{id}",Name ="GetSupCategoryById")]
        public IActionResult GetSupcategoryByid(Guid id)
        {
            var supcategory = reposatory.GetById(id);
            if (supcategory==null)
            {
            return BadRequest("ID Not Found");
            }
            return Ok(supcategory);
        }

        [HttpPost]
        public IActionResult AddSupcategory(SubCategoryCreateDTO createDTO)
        {
            SubCategory sub = new SubCategory();
            if(ModelState.IsValid)
            {
                string uploadimg = Path.Combine(env.WebRootPath, "images/subCategoryImages");
                string uniqe = Guid.NewGuid().ToString() + "_" + createDTO.Image.FileName;
                string pathfile = Path.Combine(uploadimg, uniqe);
                using (var filestream = new FileStream(pathfile, FileMode.Create))
                {
                    createDTO.Image.CopyTo(filestream);
                    filestream.Close();
                }
                sub.Name = createDTO.Name;
                sub.Image = uniqe;
                sub.Category_Id = createDTO.Category_Id;
                reposatory.Insert(sub);
                string url = Url.Link("GetSupCategoryById", new {id=sub.Id});
                return Created(url,sub);
            }

            return BadRequest(ModelState);
        }




    }
}
