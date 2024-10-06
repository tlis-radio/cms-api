using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Tlis.Cms.Domain.Entities;
using Tlis.Cms.Domain.Entities.JoinTables;

namespace Tlis.Cms.Infrastructure.Persistence.Configurations;

internal sealed class ShowEntityConfiguration : IEntityTypeConfiguration<Show>
{
    public void Configure(EntityTypeBuilder<Show> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd().HasValueGenerator((_, _) => new GuidValueGenerator());

        builder.HasIndex(x => x.Name).IsUnique();

        builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.CreatedDate).IsRequired();

        builder.HasIndex(x => x.ProfileImageId);

        builder
            .HasOne(x => x.ProfileImage)
            .WithOne()
            .HasForeignKey<Show>(x => x.ProfileImageId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(x => x.Moderators)
            .WithMany()
            .UsingEntity<ShowsUsers>();
    }
}