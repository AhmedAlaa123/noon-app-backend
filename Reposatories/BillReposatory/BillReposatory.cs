using Microsoft.EntityFrameworkCore;
using noone.Models;

namespace noone.Reposatories.BillReposatory
{
    public class BillReposatory : IReposatory<Bill>
    {
        private readonly NoonEntities _noonEntities;
        public BillReposatory(NoonEntities noonEntities)
        {
            this._noonEntities = noonEntities;
        }
        public async Task<bool> Delete(Guid Id)
        {
            try
            {
                Bill deletedBill = await this.GetById(Id);
                if (deletedBill is null)
                    return false;
                this._noonEntities.Bills.Remove(deletedBill);
                await this._noonEntities.SaveChangesAsync();
                     
            }
            catch
            {
                return false;
            }
            return true;
        }

        public  async Task<ICollection<Bill>> GetAll()
        {
            return await this._noonEntities.Bills.Include(bill=>bill.Order).ToListAsync();
        }

        public async Task<Bill> GetById(Guid Id)
        {
            return await this._noonEntities.Bills.Include(bill=>bill.Order).
                                                  FirstOrDefaultAsync(bill => bill.Id == Id);
        }

        public async Task<bool> Insert(Bill item)
        {
            try
            {
                await this._noonEntities.Bills.AddAsync(item);
                await this._noonEntities.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<bool> Update(Guid Id, Bill Item)
        {
            try
            {
                var oldBill = await this.GetById(Id);
                if (oldBill is null)
                    return false;
                oldBill.Price = Item.Price;
                oldBill.Order_Id=Item.Order_Id == null?oldBill.Order_Id:Item.Order_Id;
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
