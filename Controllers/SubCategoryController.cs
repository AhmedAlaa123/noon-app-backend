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
        //Get all SupCategories
        [HttpGet("getall")]
        public IActionResult GetallSupcategory()
        {
            var supcategories = reposatory.GetAll();
            List<SubCategoryInfoDTO> infoDTOs = new List<SubCategoryInfoDTO>();
            foreach (var supcategory in supcategories)
            {
                SubCategoryInfoDTO infoDTO = new SubCategoryInfoDTO();
                infoDTO.SubCategoryId = supcategory.Id;
                infoDTO.SubCategoryName = supcategory.Name;
                infoDTO.SubCategoryImage = supcategory.Image;
                infoDTOs.Add(infoDTO);
            }
            
            return Ok(infoDTOs);
        }
        //Get  SupCategories By ID
        [HttpGet("getby/{id}", Name = "GetSupCategoryById")]
        
        public IActionResult GetSupcategoryByid(Guid id)
        {
            var supcategory = reposatory.GetById(id);
            if (supcategory==null)
            {
            return BadRequest("ID Not Found");
            }
            SubCategoryInfoDTO infoDTO = new SubCategoryInfoDTO();
            infoDTO.SubCategoryId=supcategory.Id;
            infoDTO.SubCategoryName = supcategory.Name;
            infoDTO.SubCategoryImage = supcategory.Image;
            return Ok(infoDTO);

        }
        //Add  SupCategories
        [HttpPost]
        public IActionResult AddSupcategory(SubCategoryCreateDTO createDTO)
        {
            SubCategory sub = new SubCategory();
            if(ModelState.IsValid)
            {
                //string uploadimg = Path.Combine(env.WebRootPath, "images/subCategoryImages");
                //string uniqe = Guid.NewGuid().ToString() + "_" + createDTO.Image.FileName;
                //string pathfile = Path.Combine(uploadimg, uniqe);
                //using (var filestream = new FileStream(pathfile, FileMode.Create))
                //{
                //    createDTO.Image.CopyTo(filestream);
                //    filestream.Close();
                //}
                sub.Name = createDTO.Name;
                sub.Image =createDTO.Image;
              
                reposatory.Insert(sub);
                string url = Url.Link("GetSupCategoryById",new {id=sub.Id});
                return Created(url,sub);
            }

            return BadRequest(ModelState);
        }

        //update subcategory
        [HttpPut("{ID}")]
        public IActionResult Update([FromRoute] Guid ID, SubCategoryInfoDTO createDTO)
        {
            if (ModelState.IsValid)
            {
                SubCategory subCategoryy = new SubCategory()
                {
                    Name = createDTO.SubCategoryName,
                    Image = createDTO.SubCategoryImage
                };

                bool isUpdate = reposatory.Update(ID, subCategoryy);
                if (!isUpdate)
                 return BadRequest("Problem in Update");
            }
            return Ok("Record Updated");

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSubCategory(Guid id)
        {
            var supcategory = reposatory.GetById(id);
            if (supcategory!=null)
            {
                reposatory.Delete(id);
                return Ok("Record Deleted");
            }
            return BadRequest("ID Not Found");
        }


    }
}
