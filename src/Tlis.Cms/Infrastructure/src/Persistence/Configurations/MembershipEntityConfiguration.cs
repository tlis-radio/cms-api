using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Tlis.Cms.Domain.Constants;
using Tlis.Cms.Domain.Entities;

namespace Tlis.Cms.Infrastructure.Persistence.Configurations;

internal class MembershipEntityConfiguration : IEntityTypeConfiguration<Membership>
{
    public void Configure(EntityTypeBuilder<Membership> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd().HasValueGenerator((_, _) => new GuidValueGenerator());

        builder.Property(x => x.Status).HasConversion<string>().IsRequired();

        builder.HasIndex(x => x.Id);

        builder.HasData(new List<Membership>()
        {
            new ()
            {
                Id = MembershipStatusId.Active,
                Status = MembershipStatus.Active
            },
            new ()
            {
                Id = MembershipStatusId.Archive,
                Status = MembershipStatus.Archive
            },
            new ()
            {
                Id = MembershipStatusId.Postponed,
                Status = MembershipStatus.Postponed
            }
        });
    }
}