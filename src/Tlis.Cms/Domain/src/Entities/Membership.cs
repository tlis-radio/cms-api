using Tlis.Cms.Domain.Entities.Base;
using Tlis.Cms.Domain.Constants;

namespace Tlis.Cms.Domain.Entities;

public class Membership : BaseEntity
{
    public MembershipStatus Status { get; set; }
}