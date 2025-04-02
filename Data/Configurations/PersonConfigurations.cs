using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonAssets.Data.Entity;

namespace PersonAssets.Data.Configurations;

public class PersonConfigurations : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder
            .HasIndex(x => x.NationalCode).IsUnique(true);
        builder.HasQueryFilter(x => x.IsDeleted == false);
    }
}