using OnDemandCarWashSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnDemandCarWashSystem.Repository
{
    public interface IPackage
    {
        Task<IEnumerable<CarPackageModel>> GetAllAsync();
        Task<CarPackageModel> GetAsync(int id);
        Task<CarPackageModel> AddAsync(CarPackageModel entity);
        Task<CarPackageModel> DeleteAsync(int id);
        Task<CarPackageModel> UpdateAsync(int id, CarPackageModel entity);
    }
}
