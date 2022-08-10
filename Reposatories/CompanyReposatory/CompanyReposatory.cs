using Microsoft.EntityFrameworkCore;
using noone.Models;

namespace noone.Reposatories.CompanyReposatory
{
    public class ComponyReposatory : IReposatory<Company>
    {
        private readonly NoonEntities _noonEntities;
        public ComponyReposatory(NoonEntities noonEntities)
        {
            this._noonEntities = noonEntities;
        }
        public async Task<bool> Delete(Guid Id)
        {
            try
            {
                var Company = await this.GetById(Id);
                if (Company == null)
                    return false;
                this._noonEntities.Companies.Remove(Company);
                await this._noonEntities.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<ICollection<Company>> GetAll()
        {
            return await this._noonEntities.Companies.ToListAsync();
        }

        public async Task<Company> GetById(Guid Id)
        {
            return await this._noonEntities.Companies.FirstOrDefaultAsync(company => company.Id == Id);
        }

        public async Task<bool> Insert(Company item)
        {
            try
            {
                await this._noonEntities.Companies.AddAsync(item);
                await this._noonEntities.SaveChangesAsync();
            }
            catch
            { 
                return false;
            }
            return true;
        }

        public async Task<bool> Update(Guid Id, Company Item)
        {
            try
            {
                var Company = await this.GetById(Id);

                if (Company == null)
                    return false;
                Company.Name = Item.Name;
                Company.ContactNumber = Item.ContactNumber;
                if(Item.BrandImage!=null)
                {
                Company.BrandImage = Item.BrandImage;
                }

                await this._noonEntities.SaveChangesAsync();

            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
