using Microsoft.EntityFrameworkCore;

using noone.Models;

namespace noone.Reposatories.CateegoryReposatory
{
    public class CategoryReposatory:IReposatory<Category>
    {
        private readonly NoonEntities context;
        public CategoryReposatory(NoonEntities _context)
        {
            this.context = _context;
        }
       public async Task<bool> Insert(Category category)
        {
            if (category == null)
                return false;
            Category tempcategory = new Category();
            tempcategory.Name = category.Name;
            context.Categories.Add(tempcategory);
            await context.SaveChangesAsync();
            return true;

        }
        public async Task<bool> Delete(Guid Id)
        {
            try
            {
                Category tempcategory = context.Categories.FirstOrDefault(c=>c.Id == Id);
                if (tempcategory != null)
                {
                    context.Categories.Remove(tempcategory);
                    await context.SaveChangesAsync();

                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return false;
        }
      public async Task<bool> Update(Guid Id, Category category)
        {
                Category oldcategory = await context.Categories.FirstOrDefaultAsync(c => c.Id == Id);
                if (oldcategory != null)
                {
                    oldcategory.Name = category.Name;
                    await context.SaveChangesAsync();
                    return true;
                }
                return false;   
        }

       public async Task<Category> GetById(Guid Id)
        {
            //ListofCategoryDTO categoryDto = new ListofCategoryDTO();
           
            Category category= await context.Categories.Include(cate=>cate.SubCategories).Include(cate=>cate.Products).FirstOrDefaultAsync(c => c.Id == Id);
               
            return category;
        }
      public async Task<ICollection<Category>> GetAll()
        {
            List<Category>categories=await context.Categories.Include(c=>c.Products).Include(c=>c.SubCategories).ToListAsync();
            return categories;
        }
    }
}
