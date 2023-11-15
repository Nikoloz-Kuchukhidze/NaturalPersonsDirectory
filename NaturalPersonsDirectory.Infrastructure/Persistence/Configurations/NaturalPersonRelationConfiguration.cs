using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NaturalPersonsDirectory.Domain.Entities;
using NaturalPersonsDirectory.Domain.Enums;
using NaturalPersonsDirectory.Infrastructure.Persistence.Constants;
using System.Xml.Linq;

namespace NaturalPersonsDirectory.Infrastructure.Persistence.Configurations;

public sealed class NaturalPersonRelationConfiguration : IEntityTypeConfiguration<NaturalPersonRelation>
{
    public void Configure(EntityTypeBuilder<NaturalPersonRelation> builder)
    {
        builder.ToTable(TableName.NaturalPersonRelations);

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.RelationType)
            .IsUnicode(false)
            .IsRequired()
            .HasConversion<EnumToStringConverter<RelationType>>()
            .HasMaxLength(20);

        builder
            .HasOne(x => x.RelatedNaturalPerson)
            .WithMany()
            .HasForeignKey(x => x.RelatedNaturalPersonId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(x => x.NaturalPerson)
            .WithMany(x => x.Relations)
            .HasForeignKey(x => x.NaturalPersonId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(x => x.RelatedNaturalPerson)
            .AutoInclude();
    }
}
