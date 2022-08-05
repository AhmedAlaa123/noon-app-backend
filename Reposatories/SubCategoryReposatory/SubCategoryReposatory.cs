using Microsoft.EntityFrameworkCore;
using noone.Models;

namespace noone.Reposatories.SubCategoryReposatory
{
    public class SubCategoryReposatory : IReposatory <SubCategory>
    {
        private readonly NoonEntities noonEntities;

        public SubCategoryReposatory (NoonEntities _noonEntities)
        {
            noonEntities = _noonEntities;
        }


        public bool Delete(Guid id)
        {
            SubCategory subCategory = this.GetById(id);
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



        public ICollection<SubCategory> GetAll()
        {
            return this.noonEntities.SubCategories.ToList();

        }

        public SubCategory GetById(Guid Id)
        {
            return this.noonEntities.SubCategories.FirstOrDefault(emp => emp.Id == Id);
        }

        public bool Insert(SubCategory item)
        {
            try
            {
             

                this.noonEntities.SubCategories.Add(item);
                this.noonEntities.SaveChanges();

            }
            catch
            {
                return false;

            }
            return true;

        }

        public bool Update(Guid Id,SubCategory subCategory)
        {
            SubCategory Oldone = this.noonEntities.SubCategories.FirstOrDefault(emp => emp.Id == Id);
            if (subCategory== null)
                return false;
            try
            {
                Oldone.Name = subCategory.Name;
                Oldone.Image = subCategory.Image;
                Oldone.Category_Id = subCategory.Category_Id;
                Oldone.Products = subCategory.Products;
                this.noonEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }


    }
}
