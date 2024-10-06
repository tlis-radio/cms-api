using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.Application.Contracts.Api.Requests.Broadcasts;
using Tlis.Cms.Application.Contracts.Api.Responses;
using Tlis.Cms.Application.Mappings;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.Application.RequestHandlers.Broadcasts;

internal sealed class BroadcastCreateRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<BroadcastCreateRequest, BaseCreateResponse>
{
    public async Task<BaseCreateResponse> Handle(BroadcastCreateRequest request, CancellationToken cancellationToken)
    {
        var broadcast = BroadcastMappings.MapToBroadcast(request);

        await unitOfWork.BroadcastRepository.InsertAsync(broadcast);
        await unitOfWork.SaveChangesAsync();

        return new BaseCreateResponse
        {
            Id = broadcast.Id,
        };
    }
}