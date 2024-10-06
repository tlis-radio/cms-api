using System;
using System.Collections.Generic;
using Tlis.Cms.Domain.Entities.Base;
using Tlis.Cms.Domain.Entities.Images;
using Tlis.Cms.Domain.Entities.JoinTables;

namespace Tlis.Cms.Domain.Entities;

public class Show : BaseEntity
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateOnly CreatedDate { get; set; }

    public Guid? ProfileImageId { get; set; }

    public virtual Image? ProfileImage { get; set; }

    public virtual ICollection<User> Moderators { get; set; } = [];

    public virtual ICollection<ShowsUsers> ShowsUsers { get; set; } = [];

    public virtual ICollection<Broadcast> Broadcasts { get; set; } = [];
}