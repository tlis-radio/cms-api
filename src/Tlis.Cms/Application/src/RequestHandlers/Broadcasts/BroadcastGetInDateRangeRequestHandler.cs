using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.Application.Contracts.Api.Requests.Broadcasts;
using Tlis.Cms.Application.Contracts.Api.Responses.BroadcastGetInDateRangeResponses;
using Tlis.Cms.Application.Mappings;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.Application.RequestHandlers.Broadcasts;

internal sealed class BroadcastGetInDateRangeRequestHandler(
    IUnitOfWork unitOfWork,
    ICloudeStorageService cloudeStorageService) : IRequestHandler<BroadcastGetInDateRangeRequest, BroadcastGetInDateRangeResponse?>
{
    public async Task<BroadcastGetInDateRangeResponse?> Handle(BroadcastGetInDateRangeRequest request, CancellationToken cancellationToken)
    {
        var broadcasts = await unitOfWork.BroadcastRepository.GetInDateRangeAsync(request.From, request.To);

        return new BroadcastGetInDateRangeResponse
        {
            Results = broadcasts.Select(x => BroadcastMappings.MapToBroadcastGetInDateRangeResponse(
                x,
                cloudeStorageService.GetBroadcastImageUrl(x.Image?.FileName)
            )).ToList()
        };
    }
}