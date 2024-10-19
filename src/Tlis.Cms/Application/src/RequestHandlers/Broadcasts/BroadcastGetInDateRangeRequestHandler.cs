using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.Application.Contracts.Api.Requests.Broadcasts;
using Tlis.Cms.Application.Contracts.Api.Responses.BroadcastGetInDateRangeResponses;
using Tlis.Cms.Application.Mappings;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.Application.RequestHandlers.Broadcasts;

internal sealed class BroadcastGetInDateRangeRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<BroadcastGetInDateRangeRequest, BroadcastGetInDateRangeResponse?>
{
    public async Task<BroadcastGetInDateRangeResponse?> Handle(BroadcastGetInDateRangeRequest request, CancellationToken cancellationToken)
    {
        var broadcasts = await unitOfWork.BroadcastRepository.GetInDateRangeAsync(request.From, request.To);

        return new BroadcastGetInDateRangeResponse
        {
            Results = broadcasts.Select(BroadcastMappings.MapToBroadcastGetInDateRangeResponse).ToList()
        };
    }
}