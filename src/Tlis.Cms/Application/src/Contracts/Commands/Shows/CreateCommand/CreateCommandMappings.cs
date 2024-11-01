using System;
using System.Linq;
using Tlis.Cms.Domain.Entities;
using Tlis.Cms.Domain.Entities.JoinTables;

namespace Tlis.Cms.Application.Contracts.Commands.Shows.CreateCommand;

public static class CreateCommandMappings
{
    public static Show MapToShow(this CreateCommand request)
    {
        return new Show
        {
            Name = request.Name,
            Description = request.Description,
            CreatedDate = DateOnly.FromDateTime(DateTime.UtcNow),
            ShowsUsers = request.ModeratorIds.Select(x => new ShowsUsers { UserId = x }).ToList()
        };
    }
}