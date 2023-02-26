using Microsoft.EntityFrameworkCore;
using OnDemandCarWashSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnDemandCarWashSystem.Repository
{
    public class Car: ICar
    {
        private readonly CarWashDbContext carwashdb;
        public Car(CarWashDbContext carwashDb)
        {
            this.carwashdb = carwashDb;
        }
        public async Task<CarModel> AddAsync(CarModel car)
        {
            await carwashdb.AddAsync(car);
            await carwashdb.SaveChangesAsync();
            return car;
        }
        public async Task<CarModel> DeleteAsync(int id)
        {
            var cars = await carwashdb.CarTable.FirstOrDefaultAsync(x => x.Id == id);
            if (cars == null)
            {
                return null;
            }
            carwashdb.CarTable.Remove(cars);
            await carwashdb.SaveChangesAsync();
            return cars;
        }
        public async Task<IEnumerable<CarModel>> GetAllAsync()
        {
            var car = await carwashdb.CarTable.ToListAsync();
            return car;
        }
        public async Task<CarModel> GetAsync(int id)
        {
            return await carwashdb.CarTable.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<CarModel> UpdateAsync(int id, CarModel car)
        {
            var update = await carwashdb.CarTable.FirstOrDefaultAsync(x => x.Id == id);
            if (update == null)
            {
                return null;
            }

            update.Model = car.Model;
            
            update.Status = car.Status;
            await carwashdb.SaveChangesAsync();
            return update;
        }


    }
}
