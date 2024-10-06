using System;

namespace Tlis.Cms.Domain.Entities.JoinTables;

public class ShowsUsers
{
    public Guid ShowId { get; set; }

    public Guid UserId { get; set; }
}