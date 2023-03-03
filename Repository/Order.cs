using Microsoft.EntityFrameworkCore;
using OnDemandCarWashSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnDemandCarWashSystem.Repository
{
    public class Order:IOrder
    {
        private readonly CarWashDbContext carwashdb;
        public Order(CarWashDbContext carwashDb) { this.carwashdb = carwashDb; }
        public async Task<OrderModel> AddAsync(OrderModel order)
        {
            await carwashdb.AddAsync(order);
            await carwashdb.SaveChangesAsync();
            return order;
        }
        public async Task<OrderModel> DeleteAsync(int id)
        {
            var order = await carwashdb.OrderTable.FirstOrDefaultAsync(x => x.Id == id); if (order == null)
            {
                return null;
            }
            carwashdb.OrderTable.Remove(order);
            await carwashdb.SaveChangesAsync();
            return order;
        }
        public async Task<IEnumerable<OrderModel>> GetAllAsync()
        {
            var order = await carwashdb.OrderTable.ToListAsync();
            return order;
        }
        public async Task<OrderModel> GetAsync(int id) { return await carwashdb.OrderTable.FirstOrDefaultAsync(x => x.Id == id); }
        public async Task<OrderModel> UpdateAsync(int id, OrderModel package)
        {
            var update = await carwashdb.OrderTable.FirstOrDefaultAsync(x => x.Id == id);
            if (update == null)
            {
                return null;
            }
            update.WashingInstructions = package.WashingInstructions;
            update.Date = package.Date;
            update.status = package.status;
            update.packageName = package.packageName;
            update.price = package.price;
            update.city = package.city;
            update.pincode = package.pincode;
            await carwashdb.SaveChangesAsync();
            return update;
        }


        public async Task<OrderModel> SendOrderEmail(int id, UserModel user) {
            return await carwashdb.OrderTable.FirstOrDefaultAsync(x => x.Id == id);
        }
    }


}

