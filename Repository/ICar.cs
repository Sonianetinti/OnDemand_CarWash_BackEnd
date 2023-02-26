using OnDemandCarWashSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnDemandCarWashSystem.Repository
{
    public interface ICar
    {
        Task<IEnumerable<CarModel>> GetAllAsync();
        Task<CarModel> GetAsync(int id);
        Task<CarModel> AddAsync(CarModel entity);
        Task<CarModel> DeleteAsync(int id);
        Task<CarModel> UpdateAsync(int id, CarModel entity);
    }
}
