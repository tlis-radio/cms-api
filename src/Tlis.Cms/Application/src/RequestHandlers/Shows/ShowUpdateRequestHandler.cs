using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.Application.Contracts.Api.Requests.Shows;
using Tlis.Cms.Domain.Entities.JoinTables;
using Tlis.Cms.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.Application.RequestHandlers.Shows;

internal sealed class ShowUpdateRequestHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ShowUpdateRequest, bool>
{
    public async Task<bool> Handle(ShowUpdateRequest request, CancellationToken cancellationToken)
    {
        var toUpdate = await unitOfWork.ShowRepository.GetByIdAsync(request.Id, true);
        if (toUpdate is null)
        {
            return false;
        }

        toUpdate.Name = request.Name;
        toUpdate.Description = request.Description;

        toUpdate.ShowsUsers.Clear();
        toUpdate.ShowsUsers = request.ModeratorIds.Select(userId => new ShowsUsers { UserId = userId }).ToList();

        await unitOfWork.SaveChangesAsync();

        return true;
    }
}