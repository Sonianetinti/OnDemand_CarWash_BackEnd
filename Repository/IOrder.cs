using OnDemandCarWashSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnDemandCarWashSystem.Repository
{
    public interface IOrder
    {
        Task<IEnumerable<OrderModel>> GetAllAsync();
        Task<OrderModel> GetAsync(int id);
        Task<OrderModel> AddAsync(OrderModel entity);
        Task<OrderModel> DeleteAsync(int id);
        Task<OrderModel> UpdateAsync(int id, OrderModel entity);

    }
}
