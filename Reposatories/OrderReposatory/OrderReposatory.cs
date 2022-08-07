using Microsoft.EntityFrameworkCore;
using noone.Models;

namespace noone.Reposatories.OrderReposatory
{
    public class OrderReposatory //: IReposatory<Order>
    {
        private readonly NoonEntities _noonEntities;

        public OrderReposatory(NoonEntities noonEntities)
        {
            this._noonEntities = noonEntities;
        }

        //Insert
        public async Task<bool> Insert(Order item)
        {
            try
            {
                await this._noonEntities.Orders.AddAsync(item);
                await this._noonEntities.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        //Get all Order that ordered by user 
        public async Task<ICollection<Order>> GetAll()
        {
            return await this._noonEntities.Orders.Include(ord => ord.User).ToListAsync();
        }
       
       //Get order by ID
        public async Task<Order> GetById(Guid Id)
        {
         
            Order order = await this._noonEntities.Orders.FirstOrDefaultAsync(c => c.Id == Id);

            return order;

        }

        //Update 

        public async Task<bool> Update(Guid Id, Order Item)
        {
            try
            {
                var Order = await this.GetById(Id);

                if (Order == null)
                    return false;
                Order.OrderDate = Item.OrderDate;
                Order.DeliverDate = Item.DeliverDate;
                await this._noonEntities.SaveChangesAsync();

            }
            catch
            {
                return false;
            }
            return true;
        }


        //Delete

        public async Task<bool> Delete(Guid Id)
        {
            try
            {
                var order = await this.GetById(Id);
                if (order == null)
                    return false;
                this._noonEntities.Orders.Remove(order);
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
