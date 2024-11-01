using System;

namespace Tlis.Cms.Application.Contracts.Commands.Users.CreateCommand;

public sealed class CreateCommandMembershipHistory
{
    public required Guid MembershipId { get; set; }

    public required DateTime ChangeDate { get; set; }

    public required string Description { get; set; }
}