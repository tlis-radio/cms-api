using System;
using System.Linq;
using Tlis.Cms.Domain.Entities;
using Tlis.Cms.Domain.Entities.JoinTables;

namespace Tlis.Cms.Application.Contracts.Commands.Shows.CreateCommand;

public static class CreateCommandMappings
{
    public static Show MapToShow(this CreateCommand command)
        => new()
        {
            Name = command.Name,
            Description = command.Description,
            CreatedDate = DateOnly.FromDateTime(DateTime.UtcNow),
            ShowsUsers = command.ModeratorIds.Select(x => new ShowsUsers { UserId = x }).ToList()
        };
}