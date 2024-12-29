using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rubik_Market.Domain.Models;

namespace Rubik_Market.Infra.IOC.Context
{
    public class RubikMarketDbContext:DbContext
    {
        public RubikMarketDbContext(DbContextOptions<RubikMarketDbContext> options) :base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserProfileInfo> UserProfileInfo { get; set; }
    }
}
