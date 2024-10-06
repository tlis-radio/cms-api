using System;
using Tlis.Cms.Domain.Entities.Base;
using Tlis.Cms.Domain.Entities.Images;

namespace Tlis.Cms.Domain.Entities;

public class Broadcast : BaseEntity
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string ExternalUrl { get; set;} = null!;

    // TODO: Guests

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public Guid? ImageId { get; set; }

    public Guid ShowId { get; set; }

    public virtual Image? Image { get; set; }

    public virtual Show? Show { get; set; }
}