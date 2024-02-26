using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{

    public DbSet<AdressEntity> Adresses { get; set; } = null!;
    public DbSet<UserEntity> Users { get; set; } = null!;


}
