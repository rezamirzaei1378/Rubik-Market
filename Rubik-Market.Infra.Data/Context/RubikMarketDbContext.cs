using Microsoft.EntityFrameworkCore;
using Rubik_Market.Domain.Models;
using Rubik_Market.Domain.Models.Blog;

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
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<BlogPosts> BlogPosts { get; set; }
        public DbSet<BlogTag> BlogTagLists { get; set; }
        public DbSet<BlogPostTags> BlogPostTags { get; set; }
        public DbSet<BlogPostView> BolgPostViews { get; set; }
        public DbSet<BlogPostComment> BlogPostComments { get; set; }
        public DbSet<BlogGroup> BlogGroups { get; set; }
        public DbSet<BlogPostGroup> BlogPostGroups { get; set; }
        
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
