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

    public override int SaveChanges()
    {
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var entries = ChangeTracker.Entries<AuditableEntity>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified ||
                        e.State == EntityState.Deleted);
        //
        foreach (var entry in entries)
            if (entry.State is EntityState.Added or EntityState.Modified or EntityState.Deleted)
                switch (entry.State)
                {
                    case EntityState.Added:
                        // entry.Entity.CreatorUser = currentUser.UserId;
                        // entry.Entity.CreateDate = clock.Now;
                        // entry.Entity.IsDeleted = false;
                        // entry.Entity.Version = 0;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedBy = "";
                        entry.Entity.ModifiedDateTime = entry.Entity.ModifiedDateTime = DateTime.Now;
                        entry.Entity.Version += 1;
                        break;
                    case EntityState.Deleted:
                        // entry.Entity.IsDeleted = true;
                        // entry.Entity.ModifierUser = currentUser.UserId > 0 ? currentUser.UserId : null;
                        // entry.Entity.ModificationDate = clock.Now;
                        // entry.Entity.Version += 1;
                        entry.State = EntityState.Modified;
                        break;
                }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    public DbSet<Person> Person => Set<Person>();
    public DbSet<Car> Car => Set<Car>();
    public DbSet<PersonCar> PersonCars => Set<PersonCar>();
}