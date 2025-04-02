using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonAssets.Data.Entity;

namespace PersonAssets.Data.Configurations;

public class PersonCarConfigurations : IEntityTypeConfiguration<PersonCar>
{
    public void Configure(EntityTypeBuilder<PersonCar> builder)
    {
        builder.HasKey(x => new { x.PersonId, x.CarId }); //1
        //2
        builder
            .HasOne(x => x.Car)
            .WithMany(x => x.PersonCars)
            .HasForeignKey(x => x.CarId);
        builder
            .HasOne(x => x.Person)
            .WithMany(x => x.PersonCars)
            .HasForeignKey(x => x.PersonId);

    }
}