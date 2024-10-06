using System;
using Tlis.Cms.Domain.Constants;

namespace Tlis.Cms.Application.Contracts.Api.Responses.UserGetResponses;

public sealed class UserGetResponseUserMembershipHistoryMembership
{
    public required Guid Id { get; set; }

    public required MembershipStatus Status { get; set; }
}