using Tlis.Cms.Domain.Entities;

namespace Tlis.Cms.Application.Contracts.Commands.Broadcasts.CreateCommand;

public static class CreateCommandMappings
{
    public static Broadcast MapToBroadcast(this CreateCommand request)
    {
        return new Broadcast
        {
            Name = request.Name,
            ExternalUrl = string.Empty, // TODO: sem sa budu davat veci ako url na slido atd.
            Description = request.Description,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            ShowId = request.ShowId
        };
    }
}