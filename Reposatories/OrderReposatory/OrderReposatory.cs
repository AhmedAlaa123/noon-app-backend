﻿using Microsoft.EntityFrameworkCore;
using noone.ApplicationDTO.ProductDTO;
using noone.Models;

namespace noone.Reposatories.OrderReposatory
{
    public class OrderReposatory : IReposatory<Order>
    {
        private readonly NoonEntities _noonEntities;
        private Guid OrderID { get; set; }
        public OrderReposatory(NoonEntities noonEntities)
        {
            this._noonEntities = noonEntities;
        }

        public async Task<bool> AddProductsToOrder(List<ProductOrderCreateDTO> products)
        {
            try
            {
                // add products to order
                foreach(var pro in products)
                {
                    await this._noonEntities.ProductOrders.AddAsync(
                        new ProductOrder
                            {
                                Product_Id = pro.ProductId,
                                Order_Id = this.OrderID
                            }
                        ) ;
                    await this._noonEntities.SaveChangesAsync();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        //Insert
        public async Task<bool> Insert(Order item)
        {
            try
            {
                this.OrderID = Guid.NewGuid();
                item.Id = this.OrderID;
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
            return await this._noonEntities.Orders.
                                            Include(ord => ord.User).
                                            Include(ord=>ord.DeliverCompany).
                                            Include(ord=>ord.ProductOrders).
                                            ToListAsync();
        }
       
       //Get order by ID
        public async Task<Order> GetById(Guid Id)
        {
         
            Order order = await this._noonEntities.Orders.
                                                   Include(ord => ord.User).
                                                   Include(ord => ord.DeliverCompany).
                                                   Include(ord => ord.ProductOrders).
                                                   FirstOrDefaultAsync(c => c.Id == Id);

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
