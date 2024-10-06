using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.Application.Contracts.Api.Requests.Shows;
using Tlis.Cms.Application.Contracts.Api.Responses.ShowDetailsGetResponses;
using Tlis.Cms.Application.Mappings;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.Application.RequestHandlers.Shows;

internal sealed class ShowDetailsGetRequestHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ShowDetailsGetRequest, ShowDetailsGetResponse?>
{
    public async Task<ShowDetailsGetResponse?> Handle(ShowDetailsGetRequest request, CancellationToken cancellationToken)
    {
        var show = await unitOfWork.ShowRepository.GetByIdAsync(request.Id, false);

        if (show is null)
        {
            return null;
        }

        return ShowMappings.ToDto(show);
    }
}