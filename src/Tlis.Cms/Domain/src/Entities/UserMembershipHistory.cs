using System;
using Tlis.Cms.Domain.Entities.Base;

namespace Tlis.Cms.Domain.Entities;

public class UserMembershipHistory : BaseEntity
{
    public Guid UserId { get; set; }

    public Guid MembershipId { get; set; }

    public DateTime ChangeDate { get; set; }

    public string? Description { get; set; }

    public Membership Membership { get; set; } = null!;
}