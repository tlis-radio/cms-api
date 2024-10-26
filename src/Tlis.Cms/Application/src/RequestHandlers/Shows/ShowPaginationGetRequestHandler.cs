using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.Application.Contracts.Api.Requests.Shows;
using Tlis.Cms.Application.Contracts.Api.Responses;
using Tlis.Cms.Application.Mappings;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.Application.RequestHandlers.Shows;

internal sealed class ShowPaginationGetRequestHandler(
    IUnitOfWork unitOfWork,
    ICloudeStorageService cloudeStorageService)
    : IRequestHandler<ShowPaginationGetRequest, PaginationResponse<ShowPaginationGetResponse>>
{
    public async Task<PaginationResponse<ShowPaginationGetResponse>> Handle(ShowPaginationGetRequest request, CancellationToken cancellationToken)
    {
        var shows = await unitOfWork.ShowRepository.PaginationAsync(request.Limit, request.Page);

        return new PaginationResponse<ShowPaginationGetResponse>
        {
            Total = shows.Total,
            Limit = shows.Limit,
            Page = shows.Page,
            TotalPages = shows.TotalPages,
            Results = shows.Results.Select(x => ShowMappings.MapToShowPaginationGetResponse(
                x,
                cloudeStorageService.GetShowImageUrl(x.ProfileImage?.FileName)
            )).ToList()
        };
    }
}