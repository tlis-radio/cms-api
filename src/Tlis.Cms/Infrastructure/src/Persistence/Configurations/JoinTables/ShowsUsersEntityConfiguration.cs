using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tlis.Cms.Domain.Entities;
using Tlis.Cms.Domain.Entities.JoinTables;

namespace Tlis.Cms.Infrastructure.Persistence.Configurations.JoinTables;

public class ShowsUsersEntityConfiguration : IEntityTypeConfiguration<ShowsUsers>
{
    public void Configure(EntityTypeBuilder<ShowsUsers> builder)
    {
        builder.HasKey(x => new { x.ShowId, x.UserId });

        builder.Property(x => x.ShowId).IsRequired();
        builder.Property(x => x.UserId).IsRequired();

        builder.HasIndex(x => x.ShowId);
        builder.HasIndex(x => x.UserId);

        builder
            .HasOne<Show>()
            .WithMany(x => x.ShowsUsers)
            .HasForeignKey(x => x.ShowId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasOne<User>()
            .WithMany(x => x.ShowsUsers)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}