using System;

namespace Tlis.Cms.Application.Contracts.Commands.User.UpdateCommand;

public sealed class UpdateCommandMembershipHistory
{
    public required Guid? Id { get; set; }

    public required Guid MembershipId { get; set; }

    public required string? Description { get; set; }

    public required DateTime ChangeDate { get; set; }
}