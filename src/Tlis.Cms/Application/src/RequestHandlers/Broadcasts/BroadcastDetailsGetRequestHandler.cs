using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.Application.Contracts.Api.Requests.Broadcasts;
using Tlis.Cms.Application.Contracts.Api.Responses.BroadcastDetailsGetResponses;
using Tlis.Cms.Application.Mappings;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.Application.RequestHandlers.Broadcasts;

internal sealed class BroadcastDetailsGetRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<BroadcastDetailsGetRequest, BroadcastDetailsGetResponse?>
{
    public async Task<BroadcastDetailsGetResponse?> Handle(BroadcastDetailsGetRequest request, CancellationToken cancellationToken)
    {
        var broadcast = await unitOfWork.BroadcastRepository.GetByIdAsync(request.Id, asTracking: false);

        return BroadcastMappings.ToBroadcastDetailsGetResponse(broadcast);
    }
}