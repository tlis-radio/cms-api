using Tlis.Cms.Domain.Entities.Base;

namespace Tlis.Cms.Domain.Entities;

public class Role : BaseEntity
{
    public string Name { get; set; } = null!;

    public string ExternalId { get; set; } = null!;
}