using Microsoft.EntityFrameworkCore;
using OnDemandCarWashSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnDemandCarWashSystem.Repository
{
    public class UserRepository:ICarWash
    {
        private readonly CarWashDbContext carwashdb;
        public UserRepository(CarWashDbContext carwashDb)
        {
            this.carwashdb = carwashDb;
        }
        public async Task<UserModel> AddAsync(UserModel user)
        {
            await carwashdb.AddAsync(user);
            await carwashdb.SaveChangesAsync();
            return user;
        }
        public async Task<UserModel> DeleteAsync(int id)
        {
            var user = await carwashdb.UserTable.FirstOrDefaultAsync(x => x.UserId == id);
            if (user == null)
            {
                return null;
            }
            carwashdb.UserTable.Remove(user);
            await carwashdb.SaveChangesAsync();
            return user;
        }
        public async Task<IEnumerable<UserModel>> GetAllAsync()
        {
            var users = await carwashdb.UserTable.ToListAsync();
            return users;
        }
        public async Task<UserModel> GetAsync(int id)
        {
            return await carwashdb.UserTable.FirstOrDefaultAsync(x => x.UserId == id);
        }
        public async Task<UserModel> UpdateAsync(int id, UserModel user)
        {
            var update = await carwashdb.UserTable.FirstOrDefaultAsync(x => x.UserId == id);
            if (update == null)
            {
                return null;
            }
            update.FirstName = user.FirstName;
            update.LastName = user.LastName;
            update.PhoneNo = user.PhoneNo;
            update.Email = user.Email;
            update.Password = user.Password;
            update.ConfirmPassword = user.ConfirmPassword;
            update.Address = user.Address;
            update.Role = user.Role;
            update.Status = user.Status;
            await carwashdb.SaveChangesAsync();
            return update;
        }
        public async Task<UserModel> LoginModel(LoginModel login)
        {
           
                var users = await carwashdb.UserTable.FirstOrDefaultAsync(x => x.Email == login.Email && x.Password == login.Password);
            if(users == null)
            {
                return null;
            }
            return users;
            
        }
    }
}

