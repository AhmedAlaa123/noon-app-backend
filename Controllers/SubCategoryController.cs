using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using noone.Models;
using noone.Reposatories;
using noone.Reposatories.SubCategoryReposatory;
using noone.ApplicationDTO.SubCategoryDto;
using noone.ApplicationDTO.SubCategoryDTO;

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
        public async Task<IActionResult> GetallSupcategory()
        {
            var supcategories = await reposatory.GetAll();
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
        [HttpGet("{id}", Name = "GetSupCategoryById")]
        
        public async Task<IActionResult> GetSupcategoryByid(Guid id)
        {
            var supcategory = await reposatory.GetById(id);
            if (supcategory==null)
            {
            return BadRequest("ID Not Found");
            }
            SubCategoryInfoDTO infoDTO = new SubCategoryInfoDTO();
            infoDTO.SubCategoryId= supcategory.Id;
            infoDTO.SubCategoryName = supcategory.Name;
            infoDTO.SubCategoryImage = supcategory.Image;
            return Ok(infoDTO);

        }
        //Add  SupCategories
        [HttpPost]
        public async Task<IActionResult> AddSupcategory([F]SubCategoryCreateDTO createDTO)
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

                sub.Image = pathfile;
                //  sub.Category_Id = createDTO.Category_Id;



                bool isIsAdded=await reposatory.Insert(sub);
                if (!isIsAdded)
                    return BadRequest("حدث خطأ اعد المحاوله");
                string url = Url.Link("GetSupCategoryById",new {id=sub.Id});
                return Created(url,sub);
            }

            return BadRequest(ModelState);
        }

        //update subcategory
        [HttpPut("{ID}")]
        public async Task<IActionResult> Update([FromRoute] Guid ID, [FromRoute] SubCategoryUpdateDTO createDTO)
        {
            if (ModelState.IsValid)
            {
                SubCategory subCategoryy = new SubCategory();
                if (createDTO.SubCategoryImage != null)
                {
                    string uploadimg = Path.Combine(env.WebRootPath, "images/subCategoryImages");
                    string uniqe = Guid.NewGuid().ToString() + "_" + createDTO.SubCategoryImage.FileName;
                    string pathfile = Path.Combine(uploadimg, uniqe);
                    using (var filestream = new FileStream(pathfile, FileMode.Create))
                    {
                        createDTO.SubCategoryImage.CopyTo(filestream);
                        filestream.Close();
                    }
                    subCategoryy.Image = pathfile;

                }
                subCategoryy.Name = createDTO.SubCategoryName;

                bool isUpdate = await reposatory.Update(ID, subCategoryy);
                if (!isUpdate)
                    return BadRequest("لم يتم تحديث البيانات يرجي اعادة المحاولة ");


            }
            return Ok("تم تديث البيانات بنجاح");

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCategory(Guid id)
        {
            var supcategory = await reposatory.GetById(id);
            if (supcategory!=null)
            {
                bool isDeleted=await reposatory.Delete(id);
                if (!isDeleted)
                    return BadRequest("حدث خطأ فى عمليه الحذف اعد المحاوله");
                return Ok("تم حذف العنصر");
            }
            return BadRequest($" ليس متاح {id.ToString()}");
        }


    }
}
