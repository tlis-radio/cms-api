using Tlis.Cms.Domain.Entities;

namespace Tlis.Cms.Application.Contracts.Commands.Broadcasts.CreateCommand;

public static class CreateCommandMappings
{
    public static Broadcast MapToBroadcast(this CreateCommand command)
        => new()
        {
            Name = command.Name,
            ExternalUrl = string.Empty, // TODO: sem sa budu davat veci ako url na slido atd.
            Description = command.Description,
            StartDate = command.StartDate,
            EndDate = command.EndDate,
            ShowId = command.ShowId
        };
}