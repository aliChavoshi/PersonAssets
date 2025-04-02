using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonAssets.Data.Entity;

namespace PersonAssets.Data.Configurations;

public class CarConfigurations : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.Property(x => x.Type)
            .IsRequired();

        builder
            .Property(x => x.Name)
            .HasMaxLength(100);
        builder.HasQueryFilter(x => x.IsDeleted == false);

        builder.HasOne(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreatedBy);
        builder.HasOne(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifiedBy);

    }
}