using Microsoft.EntityFrameworkCore;
using OnDemandCarWashSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnDemandCarWashSystem.Repository
{
    public class Package:IPackage
    {
        private readonly CarWashDbContext carwashdb;
        public Package(CarWashDbContext carwashDb)
        {
            this.carwashdb = carwashDb;
        }
        public async Task<CarPackageModel> AddAsync(CarPackageModel package)
        {
            await carwashdb.AddAsync(package);
            await carwashdb.SaveChangesAsync();
            return package;
        }
        public async Task<CarPackageModel> DeleteAsync(int id)
        {
            var pack = await carwashdb.PackageTable.FirstOrDefaultAsync(x => x.Id == id);
            if (pack == null)
            {
                return null;
            }
            carwashdb.PackageTable.Remove(pack);
            await carwashdb.SaveChangesAsync();
            return pack;
        }
        public async Task<IEnumerable<CarPackageModel>> GetAllAsync()
        {
            var package = await carwashdb.PackageTable.ToListAsync();
            return package;
        }
        public async Task<CarPackageModel> GetAsync(int id)
        {
            return await carwashdb.PackageTable.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<CarPackageModel> UpdateAsync(int id, CarPackageModel package)
        {
            var update = await carwashdb.PackageTable.FirstOrDefaultAsync(x => x.Id == id);
            if (update == null)
            {
                return null;
            }
           
            update.Name = package.Name;
            update.Price = package.Price;
            update.Status = package.Status;
            await carwashdb.SaveChangesAsync();
            return update;
        }
      

        
    }
}
    

