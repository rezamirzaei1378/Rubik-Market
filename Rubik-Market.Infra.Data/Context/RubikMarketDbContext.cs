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
        public DbSet<AboutTeam> AboutTeam { get; set; }
        public DbSet<AboutUsDescription> AboutUsDescription { get; set; }
        
        #region OnModelCreating

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            #region AboutUsDescSeedData

            modelBuilder.Entity<AboutUsDescription>()
                .HasData(new AboutUsDescription()
                {
                    ID = 1,
                    AboutUsImageName = "DefaultImg",
                    CreateDate = DateTime.Now,
                    AboutUsText = "Default",
                    isDelete = false
                });

            #endregion
            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }


}
