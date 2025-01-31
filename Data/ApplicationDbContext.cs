using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PersonAssets.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        #region Person

        // builder.Entity<Person>()
        //     .HasMany(x => x.Cars)
        //     .WithOne(x => x.Person)
        //     .HasForeignKey(x => x.PersonId);

        builder.Entity<Person>()
            .HasIndex(x => x.NationalCode).IsUnique(true);

        #endregion

        #region Car

        // builder.Entity<Car>()
        //     .HasOne(x => x.Person)
        //     .WithMany(x => x.Cars)
        //     .HasForeignKey(x => x.PersonId);

        builder.Entity<Car>()
            .Property(x => x.Type)
            .IsRequired();

        builder.Entity<Car>()
            .Property(x => x.Name)
            .HasMaxLength(100);

        #endregion

        #region PersonCar

        builder.Entity<PersonCar>().HasKey(x => new { x.PersonId, x.CarId }); //1
        //2
        builder.Entity<PersonCar>()
            .HasOne(x => x.Car)
            .WithMany(x => x.PersonCars)
            .HasForeignKey(x => x.CarId);
        builder.Entity<PersonCar>()
            .HasOne(x => x.Person)
            .WithMany(x => x.PersonCars)
            .HasForeignKey(x => x.PersonId);

        #endregion
    }

    // public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Person> Person => Set<Person>();
    public DbSet<Car> Car => Set<Car>();
}