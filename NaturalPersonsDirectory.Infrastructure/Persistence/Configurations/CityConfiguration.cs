using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NaturalPersonsDirectory.Domain.Entities;
using NaturalPersonsDirectory.Infrastructure.Persistence.Constants;

namespace NaturalPersonsDirectory.Infrastructure.Persistence.Configurations;

public sealed class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable(TableName.Cities);

        builder.HasKey(x => x.Id);

        builder
           .Property(x => x.Id)
           .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}