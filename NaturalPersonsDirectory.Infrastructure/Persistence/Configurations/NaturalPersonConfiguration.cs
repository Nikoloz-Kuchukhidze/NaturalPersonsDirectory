using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NaturalPersonsDirectory.Domain.Entities;
using NaturalPersonsDirectory.Domain.Enums;
using NaturalPersonsDirectory.Infrastructure.Persistence.Constants;
using NaturalPersonsDirectory.Infrastructure.Persistence.Converters;

namespace NaturalPersonsDirectory.Infrastructure.Persistence.Configurations;

public sealed class NaturalPersonConfiguration : IEntityTypeConfiguration<NaturalPerson>
{
    public void Configure(EntityTypeBuilder<NaturalPerson> builder)
    {
        builder.ToTable(TableName.NaturalPersons);

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder
            .Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(x => x.Gender)
            .IsRequired()
            .IsUnicode(false)
            .HasConversion<EnumToStringConverter<Gender>>()
            .HasMaxLength(6);

        builder
            .Property(x => x.PersonalNumber)
            .IsUnicode(false)
            .IsRequired()
            .HasMaxLength(11)
            .IsFixedLength();

        builder
            .HasIndex(x => x.PersonalNumber)
            .IsUnique();

        builder
            .Property(x => x.BirthDate)
            .IsRequired()
            .HasConversion<DateOnlyConverter>()
            .HasColumnType(SqlDataType.Date);

        builder
            .Property(x => x.IsActive)
            .HasDefaultValue(true);

        builder
            .Property(x => x.Image)
            .IsUnicode(false)
            .HasMaxLength(100);

        builder
            .HasMany(x => x.Phones)
            .WithOne(x => x.NaturalPerson)
            .HasForeignKey(x => x.NaturalPersonId);

        builder.HasOne(x => x.City)
            .WithMany()
            .HasForeignKey(x => x.CityId);
    } 
}
