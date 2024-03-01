//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
//using Infrastructure.Models;
//using Infrastructure.Entities;

//namespace Infrastructure.Contexts
//{
//    public class DataContext : IdentityDbContext<ApplicationUser>
//    {
//        public DataContext(DbContextOptions<DataContext> options)
//            : base(options)
//        {
//        }

//        public DbSet<AddressEntity> Addresses { get; set; }
//        public DbSet<FeatureEntity> Features { get; set; }
//        public DbSet<FeatureItemEntity> FeatureItems { get; set; }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);

//            modelBuilder.Entity<ApplicationUser>(entity =>
//            {
//                entity.HasOne<AddressEntity>()
//                      .WithMany()
//                      .HasForeignKey(a => a.Id);
//            });
//        }
//    }
//}



using Infrastructure.Entities;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<ApplicationUser>(options)
{

    public DbSet<AddressEntity> Addresses { get; set; } = null!;
    public DbSet<UserEntity> Users { get; set; } = null!;


    public DbSet<FeatureEntity> Features { get; set; }
    public DbSet<FeatureItemEntity> FeaturesItems { get; set; }

}




//using Infrastructure.Entities;
//using Microsoft.EntityFrameworkCore;


//namespace Infrastructure.Contexts;

//public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
//{

//    public DbSet<AddressEntity> Addresses { get; set; } = null!;
//    public DbSet<UserEntity> Users { get; set; } = null!;

//    public DbSet<FeatureEntity> Features { get; set; }
//    public DbSet<FeatureItemEntity> FeaturesItems { get; set; }

//}

