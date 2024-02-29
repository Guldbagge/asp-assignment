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

