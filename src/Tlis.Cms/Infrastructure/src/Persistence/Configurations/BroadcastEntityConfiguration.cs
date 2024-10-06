using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Tlis.Cms.Domain.Entities;

namespace Tlis.Cms.Infrastructure.Persistence.Configurations;

public class BroadcastEntityConfiguration : IEntityTypeConfiguration<Broadcast>
{
    public void Configure(EntityTypeBuilder<Broadcast> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd().HasValueGenerator((_, _) => new GuidValueGenerator());

        builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.ShowId).IsRequired();
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.ImageId);
        builder.Property(x => x.ExternalUrl);
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.StartDate).IsRequired();
        builder.Property(x => x.EndDate).IsRequired();

        builder.HasIndex(x => x.ShowId);

        builder
            .HasOne(x => x.Image)
            .WithOne()
            .HasForeignKey<Broadcast>(x => x.ImageId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.Show)
            .WithMany(x => x.Broadcasts)
            .HasForeignKey(x => x.ShowId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}