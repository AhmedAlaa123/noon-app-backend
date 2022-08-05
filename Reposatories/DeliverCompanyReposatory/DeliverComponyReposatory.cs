using Microsoft.EntityFrameworkCore;
using noone.Models;

namespace noone.Reposatories.DeliverCompanyReposatory
{
    public class DeliverComponyReposatory : IReposatory<DeliverCompany>
    {
        private readonly NoonEntities _noonEntities;
        public DeliverComponyReposatory(NoonEntities noonEntities)
        {
            this._noonEntities = noonEntities;
        }
        public async Task<bool> Delete(Guid Id)
        {
            try
            {
                var deliverCompany = await this.GetById(Id);
                if (deliverCompany == null)
                    return false;
                this._noonEntities.DeliverCompanies.Remove(deliverCompany);
                await this._noonEntities.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<ICollection<DeliverCompany>> GetAll()
        {
            return await this._noonEntities.DeliverCompanies.Include(comp=>comp.Orders).ToListAsync();
        }

        public async Task<DeliverCompany> GetById(Guid Id)
        {
            return await this._noonEntities.DeliverCompanies.Include(comp=>comp.Orders).FirstOrDefaultAsync(company=>company.Id== Id);
        }

        public async Task<bool> Insert(DeliverCompany item)
        {
            try
            {
                await this._noonEntities.DeliverCompanies.AddAsync(item);
                await this._noonEntities.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<bool> Update(Guid Id, DeliverCompany Item)
        {
            try
            {
                var deliverCompany = await this.GetById(Id);

                if (deliverCompany == null)
                    return false;
                deliverCompany.Name = Item.Name;
                deliverCompany.Address = Item.Address;
                deliverCompany.ContactNumber = Item.ContactNumber;

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
