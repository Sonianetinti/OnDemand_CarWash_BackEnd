using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnDemandCarWashSystem.Models
{
    public class CarWashDbContext:DbContext
    {
            public CarWashDbContext(DbContextOptions<CarWashDbContext> options) : base(options)
            {

            }
            public DbSet<UserModel> UserTable { get; set; }
            public DbSet<CarModel> CarTable { get; set; }
            public DbSet<CarPackageModel> PackageTable { get; set; }
    }
}

