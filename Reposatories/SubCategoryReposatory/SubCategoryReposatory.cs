using Microsoft.EntityFrameworkCore;
using noone.Models;

namespace noone.Reposatories.SubCategoryReposatory
{
    public class SubCategoryReposatory : IReposatory<SubCategory>
    {
        private readonly NoonEntities noonEntities;

        public SubCategoryReposatory (NoonEntities _noonEntities)
        {
            noonEntities = _noonEntities;
        }


        public async Task<bool> Delete(Guid id)
        {
            SubCategory subCategory = await this.GetById(id);
            if (subCategory == null)
                return false;
            try
            {
                this.noonEntities.SubCategories.Remove(subCategory);
                this.noonEntities.SaveChanges();

            }
            catch (Exception ex)
            {

                return false;
            }
            return true;

        }



        public async Task<ICollection<SubCategory>> GetAll()
        {
            return await this.noonEntities.SubCategories.Include(sub=>sub.Products).ToListAsync();

        }

        public async Task<SubCategory> GetById(Guid Id)
        {
            return await this.noonEntities.SubCategories.Include(sub=>sub.Products).FirstOrDefaultAsync(emp => emp.Id == Id);
        }

        public async Task<bool> Insert(SubCategory item)
        {
            try
            {
             

                this.noonEntities.SubCategories.Add(item);
               await this.noonEntities.SaveChangesAsync();

            }
            catch
            {
                return false;

            }
            return true;

        }

        public async Task<bool> Update(Guid Id,SubCategory subCategory)
        {
            SubCategory Oldone = await this.noonEntities.SubCategories.FirstOrDefaultAsync(emp => emp.Id == Id);
            if (subCategory== null)
                return false;
            try
            {
                Oldone.Name = subCategory.Name;
                if(subCategory.Image!=null)
                {
                Oldone.Image = subCategory.Image;
                }
                Oldone.Category_Id = subCategory.Category_Id;
                Oldone.Products = subCategory.Products;
               await this.noonEntities.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }


    }
}
