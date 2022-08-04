using Microsoft.EntityFrameworkCore;
using noone.DTO.categoryDTO;
using noone.Models;

namespace noone.Reposatories.CateegoryReposatory
{
    public class CategoryReposatory:ICategoryReposatory
    {
        private readonly NoonEntities context;
        public CategoryReposatory(NoonEntities _context)
        {
            this.context = _context;
        }
       public bool Insert(AddcategoryDTO category)
        {
            if (category == null)
                return false;
            Category tempcategory = new Category();
            tempcategory.Name = category.Name;
            context.Categories.Add(tempcategory);
            context.SaveChanges();
            return true;

        }
        public bool Delete(Guid Id)
        {
            try
            {
                Category tempcategory = context.Categories.FirstOrDefault(c=>c.Id==Id);
                if (tempcategory != null)
                {
                    context.Categories.Remove(tempcategory);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return false;
        }
      public bool Update(Guid Id, AddcategoryDTO category)
        {
           
                Category oldcategory = context.Categories.FirstOrDefault(c => c.Id == Id);
                if (oldcategory != null)
                {
                    oldcategory.Name = category.Name;
                    context.SaveChanges();
                    return true;
                }
                return false;   
                

            
         
            }

       public ListofCategoryDTO GetById(Guid Id)
        {
            ListofCategoryDTO categoryDto = new ListofCategoryDTO();
           
              categoryDto.Name= context.Categories.FirstOrDefault(c => c.Id == Id).Name;
               categoryDto.Id=context.Categories.FirstOrDefault(c=>c.Id == Id).Id;
            return categoryDto;
        }
      public ICollection<ListofCategoryDTO> GetAll()
        {
         List<Category>categories=context.Categories.Include(c=>c.Products).Include(c=>c.SubCategories).ToList();
         List<ListofCategoryDTO> list = new List<ListofCategoryDTO>();
            foreach(var category in categories)
            {
                ListofCategoryDTO tempCategory = new ListofCategoryDTO() { Name = category.Name, Id = category.Id };
             
            foreach(var product in category.Products)
                    tempCategory.productsname.Add(product.Name);
            foreach (var subCategory in category.SubCategories)
                    tempCategory.subCategoryName.Add(subCategory.Name);
           list.Add(tempCategory);
            }
            return list;
        }
    }
}
