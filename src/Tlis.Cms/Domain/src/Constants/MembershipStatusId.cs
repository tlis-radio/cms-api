using System;

namespace Tlis.Cms.Domain.Constants;

public static class MembershipStatusId
{
    public static readonly Guid Active = Guid.Parse("80126b05-9dab-4709-aa6a-39baa5bafe79");

    public static readonly Guid Archive = Guid.Parse("a7c0bea2-2812-40b6-9836-d4b5accae95a");

    public static readonly Guid Postponed = Guid.Parse("cfaeecff-d26b-44f2-bfa1-c80ab79983a9");
}