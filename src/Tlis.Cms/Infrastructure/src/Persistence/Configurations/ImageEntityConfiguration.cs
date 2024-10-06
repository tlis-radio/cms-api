using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Tlis.Cms.Domain.Entities.Images;

namespace Tlis.Cms.Infrastructure.Persistence.Configurations;

public class ImageEntityConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd().HasValueGenerator((_, _) => new GuidValueGenerator());

        builder.Property(x => x.Width).IsRequired();
        builder.Property(x => x.Height).IsRequired();
        builder.Property(x => x.Url).IsRequired();
        builder.Property(x => x.Size).IsRequired();

        builder
            .HasMany(x => x.Crops)
            .WithOne()
            .HasForeignKey(x => x.ImageId)
            .IsRequired();
    }
}