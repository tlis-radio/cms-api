using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.Application.Contracts.Api.Requests.Shows;
using Tlis.Cms.Application.Contracts.Api.Responses;
using Tlis.Cms.Application.Mappings;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.Application.RequestHandlers.Shows;

internal sealed class ShowCreateRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<ShowCreateRequest, BaseCreateResponse>
{
    public async Task<BaseCreateResponse> Handle(ShowCreateRequest request, CancellationToken cancellationToken)
    {   
        var toCreate = ShowMappings.ToEntity(request);

        await unitOfWork.ShowRepository.InsertAsync(toCreate);
        await unitOfWork.SaveChangesAsync();

        return new BaseCreateResponse
        {
            Id = toCreate.Id
        };
    }
}