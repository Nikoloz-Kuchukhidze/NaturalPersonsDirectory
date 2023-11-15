using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NaturalPersonsDirectory.Domain.Entities;
using NaturalPersonsDirectory.Domain.Enums;
using NaturalPersonsDirectory.Infrastructure.Persistence.Constants;

namespace NaturalPersonsDirectory.Infrastructure.Persistence.Configurations;

public sealed class PhoneConfiguration : IEntityTypeConfiguration<Phone>
{
    public void Configure(EntityTypeBuilder<Phone> builder)
    {
        builder.ToTable(TableName.Phones);

        builder.Property(x => x.Type)
            .IsRequired()
            .IsUnicode(false)
            .HasConversion<EnumToStringConverter<PhoneType>>()
            .HasMaxLength(20);

        builder.Property(x => x.Number)
            .IsRequired()
            .HasMaxLength(50)
            .HasAnnotation(Annotation.MinLength, 4);

        builder.HasIndex(x => x.Number)
            .IsUnique();
    }
}
