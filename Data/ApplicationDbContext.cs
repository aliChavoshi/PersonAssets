using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonAssets.Data.Entity;
using System.Reflection.Emit;
using System.Reflection;

namespace PersonAssets.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser, IdentityRole, string>(options)
{
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     //My Code
    //     // Add services to the container.
    //     // var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
    //     //                        throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    //     //
    //     // builder.Services.AddDbContext<ApplicationDbContext>(options =>
    //     //     options.UseSqlServer(connectionString));
    //     optionsBuilder.UseSqlServer("Server=.;Database=PersonAssets;Trusted_Connection=True;MultipleActiveResultSets=true");
    //     base.OnConfiguring(optionsBuilder);
    // }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    public DbSet<Person> Person => Set<Person>();
    public DbSet<Car> Car => Set<Car>();
    public DbSet<PersonCar> PersonCars => Set<PersonCar>();
}